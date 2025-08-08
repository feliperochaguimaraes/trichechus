// Application/Services/SGAAutenticacaoService.cs
using Microsoft.AspNetCore.Http;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;

namespace Trichechus.Application.Services;

public class SGAAutenticacaoService : ISGAAutenticacaoService
{
	private readonly ISGAClient _sgaClient;
	private readonly ITokenService _tokenService;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public SGAAutenticacaoService(
		ISGAClient sgaClient,
		ITokenService tokenService,
		IHttpContextAccessor httpContextAccessor)
	{
		_sgaClient = sgaClient;
		_tokenService = tokenService;
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<Result<UsuarioTokenDTO>> LoginAsync(LoginUsuarioDTO dto)
	{
		// Obter o IP do usuário
		var ipUsuario = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "::1";

		// Autenticar no SGA
		var sgaResult = await _sgaClient.AutenticarUsuarioAsync(dto.Email, dto.Senha, ipUsuario);

		if (!sgaResult.Sucesso)
		{
			return Result<UsuarioTokenDTO>.Failure(new[] { sgaResult.Mensagem! });
		}

		// Criar um usuário virtual com as informações do SGA
		var usuario = new UsuarioSGA
		{
			Id = Guid.NewGuid(), // Ou use o idusuario do SGA se preferir: Guid.TryParse(sgaResult.IdUsuario, out var id) ? id : Guid.NewGuid(),
			Nome = sgaResult.Nome,
			Email = sgaResult.Email,
			Perfil = sgaResult.Perfil, // Contém a string de nomegrupo
									   // Mapear a string de nomegrupo para roles
			// Roles = MapearNomeGrupoParaRoles(sgaResult.Perfil),
			Roles = sgaResult.PerfilDetalhes
		};

		// Gerar token JWT
		var tokenDTO = _tokenService.GerarToken(usuario);

		return Result<UsuarioTokenDTO>.Success(tokenDTO);
	}

	// Método ajustado para mapear a string de nomegrupo
	private List<string> MapearNomeGrupoParaRoles(string nomeGrupoString)
	{
		var roles = new List<string>();
		if (string.IsNullOrWhiteSpace(nomeGrupoString))
		{
			return roles; // Retorna lista vazia se não houver grupos
		}

		// Separa os nomes dos grupos pela vírgula
		var grupos = nomeGrupoString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		// Lógica de mapeamento (ajuste conforme suas necessidades)
		foreach (var grupo in grupos)
		{
			if (grupo.Contains("Administrador", StringComparison.OrdinalIgnoreCase) || grupo.Contains("Admin", StringComparison.OrdinalIgnoreCase))
			{
				if (!roles.Contains("Admin")) roles.Add("Admin");
			}
			else if (grupo.Contains("Gestor", StringComparison.OrdinalIgnoreCase))
			{
				if (!roles.Contains("Gestor")) roles.Add("Gestor");
			}
			// Adicione outras regras de mapeamento conforme necessário
			// Ex: else if (grupo.Contains("Consulta")) { if (!roles.Contains("Consulta")) roles.Add("Consulta"); }

			// Adiciona uma role padrão 'Usuario' se o usuário pertence a algum grupo
			if (!roles.Contains("Usuario")) roles.Add("Usuario");
		}

		// Garante que 'Usuario' seja adicionado se a lista estiver vazia mas havia grupos
		if (grupos.Length > 0 && roles.Count == 0)
		{
			roles.Add("Usuario");
		}

		return roles;
	}
}

public interface ISGAAutenticacaoService
{
	Task<Result<UsuarioTokenDTO>> LoginAsync(LoginUsuarioDTO dto);
}

// Classe para representar um usuário autenticado pelo SGA
public class UsuarioSGA
{
	public Guid Id { get; set; }
	public string? Nome { get; set; }
	public string? Email { get; set; }
	public string? Perfil { get; set; }
	public List<string> Roles { get; set; } = new List<string>();
	public List<string> Perfis { get; set; } = new List<string>();
}
