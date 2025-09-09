using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class URL
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(100)]
	[Column("Endereco", TypeName = "varchar(100)")]
	public required string Endereco { get; set; }

	[Required]
	[StringLength(10)]
	[Column("Ambiente", TypeName = "varchar(10)")]
	public required string Ambiente { get; set; } //DEV|PROD

	[StringLength(30)]
	[Column("Servidor", TypeName = "varchar(30)")]
	public required string Servidor { get; set; }

	[StringLength(20)]
	[Column("IP", TypeName = "varchar(20)")]
	public required string IP { get; set; }

	public ICollection<Software> Software { get; set; } = new List<Software>();
}