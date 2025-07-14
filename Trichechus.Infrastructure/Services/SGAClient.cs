using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace Trichechus.Infrastructure.Services;

public class SGAClient : ISGAClient
{
	private readonly HttpClient _httpClient;
	private readonly string _sgaEndpoint;
	private readonly string _matriculaSistema;

	public SGAClient(HttpClient httpClient, IConfiguration configuration)
	{
		_httpClient = httpClient;
		_sgaEndpoint = configuration["SGA:Endpoint"];
		_matriculaSistema = configuration["SGA:MatriculaSistema"];
	}

	public async Task<SGAAutenticacaoResult> AutenticarUsuarioAsync(string usuario, string senha, string ipUsuario)
	{
		try
		{
			// Construir o XML de requisição
			var xmlRequisicao = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<requisicao xmlns=""http://ntconsult.com.br/webservices/"" versao=""0.10"">
<usr>{usuario}</usr>
<senha>{senha}</senha>
<matricsistema>{_matriculaSistema}</matricsistema>
<usuariorede>BANPARA\{usuario}</usuariorede>
<ipusuario>{ipUsuario}</ipusuario>
</requisicao>";

			// Construir o envelope SOAP
			var soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://ntconsult.com.br/webservices/"">
   <soapenv:Header/>
   <soapenv:Body>
	  <web:Executar>
		 <web:strOperacao>Autenticar</web:strOperacao>
		 <web:strXmlRequisicao><![CDATA[{xmlRequisicao}]]></web:strXmlRequisicao>
	  </web:Executar>
   </soapenv:Body>
</soapenv:Envelope>";

			// Preparar a requisição HTTP
			var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
			content.Headers.Add("SOAPAction", "http://ntconsult.com.br/webservices/Executar");

			// Enviar a requisição
			var response = await _httpClient.PostAsync(_sgaEndpoint, content);
			response.EnsureSuccessStatusCode();

			// Ler a resposta
			var responseContent = await response.Content.ReadAsStringAsync();

			// Processar a resposta XML
			return ProcessarRespostaSGA(responseContent);
		}
		catch (Exception ex)
		{
			// Log da exceção
			Console.WriteLine($"Erro ao autenticar no SGA: {ex.Message}");
			return new SGAAutenticacaoResult
			{
				Sucesso = false,
				Mensagem = $"Erro ao autenticar no SGA: {ex.Message}"
			};
		}
	}

	private SGAAutenticacaoResult ProcessarRespostaSGA(string xmlResponse)
	{
		string resultXml = null; // Variável para armazenar o XML interno para logs
		try
		{
			// Extrair o conteúdo XML da resposta SOAP
			var soapDoc = new XmlDocument();
			soapDoc.LoadXml(xmlResponse);

			// Namespace manager para o envelope SOAP
			var soapNsManager = new XmlNamespaceManager(soapDoc.NameTable);
			soapNsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
			soapNsManager.AddNamespace("web", "http://ntconsult.com.br/webservices/");

			// Extrair o XML de resposta do CDATA
			var xmlResultNode = soapDoc.SelectSingleNode("//soap:Body/web:ExecutarResponse/web:ExecutarResult", soapNsManager);
			if (xmlResultNode == null)
			{
				return new SGAAutenticacaoResult { Sucesso = false, Mensagem = "Formato de resposta SOAP inválido (ExecutarResult não encontrado)" };
			}

			resultXml = xmlResultNode.InnerText; // Armazena para possível log
			var resultDoc = new XmlDocument();
			resultDoc.LoadXml(resultXml);

			// Namespace manager para o XML de resposta interno (<dados>)
			var resultNsManager = new XmlNamespaceManager(resultDoc.NameTable);
			resultNsManager.AddNamespace("res", "http://ntconsult.com.br/webservices/");

			// Verificar se a autenticação foi bem-sucedida
			var autenticadoNode = resultDoc.SelectSingleNode("//res:retorno/res:autenticado", resultNsManager);
			var msgRetornoNode = resultDoc.SelectSingleNode("//res:retorno/res:msgretorno", resultNsManager);
			string msgRetorno = msgRetornoNode?.InnerText ?? "Mensagem de retorno não encontrada.";

			if (autenticadoNode == null || !bool.TryParse(autenticadoNode.InnerText, out bool autenticado) || !autenticado)
			{
				return new SGAAutenticacaoResult { Sucesso = false, Mensagem = msgRetorno };
			}

			// Extrair informações do usuário
			var nomeNode = resultDoc.SelectSingleNode("//res:infousuario/res:nome", resultNsManager);
			var emailNode = resultDoc.SelectSingleNode("//res:infousuario/res:email", resultNsManager);
			var nomeGrupoNode = resultDoc.SelectSingleNode("//res:infousuario/res:nomegrupo", resultNsManager); // Usar nomegrupo para perfil/roles

			var result = new SGAAutenticacaoResult
			{
				Sucesso = true,
				Mensagem = msgRetorno, // Mensagem de sucesso
				Nome = nomeNode?.InnerText,
				Email = emailNode?.InnerText,
				// Usaremos nomegrupo para mapear roles posteriormente
				Perfil = nomeGrupoNode?.InnerText,
				// Limpamos PerfilDetalhes pois não usaremos permissoesobjetos diretamente para roles
				PerfilDetalhes = new List<string>()
			};

			// Opcional: Se precisar das permissões detalhadas de 'permissoesobjetos' para outra finalidade,
			// você pode extraí-las aqui de forma similar, mas não as usaremos para roles diretamente.
			// Exemplo:
			var permissoesNodes = resultDoc.SelectNodes("//res:permissoesobjetos", resultNsManager);
			if (permissoesNodes != null)
			{
				foreach (XmlNode node in permissoesNodes)
				{
					var nomeObjeto = node.SelectSingleNode("res:nomeobjeto", resultNsManager)?.InnerText;
					if (!string.IsNullOrEmpty(nomeObjeto))
					{
						result.PerfilDetalhes.Add(nomeObjeto); // Ou outra lógica
					}
				}
			}

			return result;
		}
		catch (XmlException xmlEx)
		{
			Console.WriteLine($"Erro ao processar XML da resposta do SGA: {xmlEx.Message}");
			Console.WriteLine($"XML Recebido (CDATA): {resultXml ?? "N/A"}");
			return new SGAAutenticacaoResult
			{
				Sucesso = false,
				Mensagem = $"Erro ao processar XML da resposta do SGA: {xmlEx.Message}"
			};
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro inesperado ao processar resposta do SGA: {ex.Message}");
			return new SGAAutenticacaoResult
			{
				Sucesso = false,
				Mensagem = $"Erro inesperado ao processar resposta do SGA: {ex.Message}"
			};
		}
	}
}

public interface ISGAClient
{
	Task<SGAAutenticacaoResult> AutenticarUsuarioAsync(string usuario, string senha, string ipUsuario);
}

public class SGAAutenticacaoResult
{
	public bool Sucesso { get; set; }
	public string Mensagem { get; set; }
	public string Nome { get; set; }
	public string Email { get; set; }
	public string Perfil { get; set; }
	public List<string> PerfilDetalhes { get; set; } = new List<string>();
}
