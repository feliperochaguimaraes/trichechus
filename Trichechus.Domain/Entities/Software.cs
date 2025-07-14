using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Software
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(50)]
	[Column("ProdutoSoftware", TypeName = "varchar(50)")]
	public string ProdutoSoftware { get; set; } = "";

	[Required]
	[StringLength(100)]
	[Column("Nome", TypeName = "varchar(100)")]
	public required string Nome { get; set; }

	[Required]
	[StringLength(20)]
	[Column("Situacao", TypeName = "varchar(20)")]
	public required string Situacao { get; set; }

	[Column("Descricao", TypeName = "Text")]
	public string? Descricao { get; set; } = null;

	[Column("Segmento", TypeName = "varchar(20)")]
	public string Segmento { get; set; } = "";

	[Column("Tipo", TypeName = "varchar(30)")]
	public string Tipo { get; set; } = ""; //Desktop|Web|API|WEBSERVICE|Serviço Windows|Serviço

	[Column("LicencaSoftware", TypeName = "varchar(3)")]
	public string LicencaSoftware { get; set; } = ""; //Sim|Não

	[Column("Tecnologia", TypeName = "Text")]
	public string Tecnologia { get; set; } = "";

	[Column("Descontinuado", TypeName = "DateTime2")]
	public DateTime Descontinuado { get; set; }

	[Column("EntrarCatalogo", TypeName = "varchar(3)")]
	public string EntrarCatalogo { get; set; } = ""; //Sim|Não

	public ICollection<Repositorio>? Repositorio { get; set; } = null;
	public ICollection<URL>? URL { get; set; } = null;
	public ICollection<BaseDados>? BaseDados { get; set; } = null;
	public ICollection<Catalogo>? Catalogo { get; set; } = null;
}