using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces;
public interface ISGAClient
{
	Task<SGAAutenticacaoResult> AutenticarUsuarioAsync(string usuario, string senha, string ipUsuario);
}

public class SGAAutenticacaoResult
{
	public bool Sucesso { get; set; }
	public string? Mensagem { get; set; }
	public string? Nome { get; set; }
	public string? Email { get; set; }
	public string? Perfil { get; set; }
	public List<string> PerfilDetalhes { get; set; } = new List<string>();
}
