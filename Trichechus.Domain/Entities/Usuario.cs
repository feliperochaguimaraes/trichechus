using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Usuario
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Nome { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string Matricula { get; set; } = default!;
	public string SenhaHash { get; set; } = default!; // Armazenar o hash da senha
	public bool Ativo { get; set; } = true;

	[Column("CriadoEm", TypeName = "DateTime2")]
	public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

	[Column("AtualizadoEm", TypeName = "DateTime2")]
	public DateTime? AtualizadoEm { get; set; }

	// Relação obrigatória com Atividade
	public Guid GerenciaId { get; set; }
	public Gerencia Gerencia { get; set; } = default!;

	// Navegação para Perfis (Muitos-para-Muitos)
	public ICollection<Perfil> Perfil { get; set; } = new List<Perfil>();
}