using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Gerencia
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(50)]
	[Column("NomeGerencia", TypeName = "varchar(50)")]
	public required string NomeGerencia { get; set; }

	[Required]
	[StringLength(30)]
	[Column("Superintendencia", TypeName = "varchar(50)")]
	public required string Superintendencia { get; set; }

	public ICollection<Software>? Software { get; set; } = null;
	public ICollection<Contrato>? Contrato { get; set; } = null;
	public ICollection<Usuario>? Usuario { get; set; } = null;
}