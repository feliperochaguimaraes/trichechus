using System.ComponentModel.DataAnnotations.Schema;
namespace Trichechus.Domain.Entities;

public class Contrato
{
	public Guid Id { get; set; } = Guid.NewGuid();
	[Column("NomeAlias", TypeName = "varchar(20)")]
	public string NomeAlias { get; set; } = "";
	[Column("Objeto", TypeName = "Text")]
	public string Objeto { get; set; } = "";
	public bool Ativo { get; set; } = true;

	[Column("Inicio", TypeName = "DateTime2")]
	public DateTime Inicio { get; set; }

	[Column("Fim", TypeName = "DateTime2")]
	public DateTime Fim { get; set; }

	[Column("AreaGestora", TypeName = "varchar(10)")]
	public string AreaGestora { get; set; } = "";

	[Column("Gerencia", TypeName = "varchar(10)")]
	public string Gerencia { get; set; } = "";

	public ICollection<Fornecedor> Fornecedor { get; set; } = new List<Fornecedor>();
	public ICollection<Software> Software { get; set; } = new List<Software>();
}