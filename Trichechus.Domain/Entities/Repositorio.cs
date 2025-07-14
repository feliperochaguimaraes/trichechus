using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Repositorio
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(100)]
	[Column("Endereco", TypeName = "varchar(100)")]
	public required string Endereco { get; set; }

	[Required]
	[StringLength(20)]
	[Column("Tipo", TypeName = "varchar(20)")]
	public required string Tipo { get; set; } //SVN|GIT

}