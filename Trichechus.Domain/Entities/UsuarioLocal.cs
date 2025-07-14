using System.ComponentModel.DataAnnotations.Schema;
namespace Trichechus.Domain.Entities;

public class UsuarioLocal
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Nome { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string SenhaHash { get; set; } = default!; // Armazenar o hash da senha
	public bool Ativo { get; set; } = true;

	[Column("CriadoEm", TypeName = "DateTime2")]
	public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

	[Column("AtualizadoEm", TypeName = "DateTime2")]
	public DateTime? AtualizadoEm { get; set; }

	// Navegação para Perfis (Muitos-para-Muitos)
	public ICollection<Perfil> Perfis { get; set; } = new List<Perfil>();
}