using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Gerencia
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(50)]
	[Column("Gerencia", TypeName = "varchar(50)")]
	public required string Cluster { get; set; }

	[Required]
	[StringLength(30)]
	[Column("Superintendencia", TypeName = "varchar(50)")]
	public required string NomeBaseDados { get; set; }

	public ICollection<Software>? Software { get; set; } = null;
}