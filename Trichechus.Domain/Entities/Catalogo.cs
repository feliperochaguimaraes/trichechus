using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Catalogo
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[StringLength(20)]
	[Column("HelixId", TypeName = "varchar(20)")]
	public string? HelixId { get; set; } = null;

	[StringLength(100)]
	[Column("HelixEquipe", TypeName = "varchar(100)")]
	public string? HelixEquipe { get; set; } = null;

	[StringLength(100)]
	[Column("HelixService", TypeName = "varchar(100)")]
	public string? HelixService { get; set; } = null;

	[StringLength(100)]
	[Column("HelixCategoria", TypeName = "varchar(100)")]
	public string? HelixCategoria { get; set; } = null;

	[StringLength(100)]
	[Column("HelixSubcategoria", TypeName = "varchar(100)")]
	public string? HelixSubcategoria { get; set; } = null;

	[StringLength(100)]
	[Column("CatalogoEquipe", TypeName = "varchar(100)")]
	public string CatalogoEquipe { get; set; } = "";

	[StringLength(3)]
	[Column("Ativo", TypeName = "varchar(3)")]
	public string Ativo { get; set; } = ""; //Sim|Não

	[Column("Observacao", TypeName = "Text")]
	public string? Observacao { get; set; } = null;

	// Relação obrigatória com Atividade
	public Guid SoftwareId { get; set; }
	public Software Software { get; set; } = default!;
}