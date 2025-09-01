using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class BaseDados
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	[StringLength(50)]
	[Column("Cluster", TypeName = "varchar(50)")]
	public required string Cluster { get; set; }

	[Required]
	[StringLength(30)]
	[Column("NomeBaseDados", TypeName = "varchar(30)")]
	public required string NomeBaseDados { get; set; }

	[StringLength(30)]
	[Column("Versao", TypeName = "varchar(30)")]
	public required string Versao { get; set; }
	
	public ICollection<Software> Software { get; set; } =  new List<Software>();
}