using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Tarefa
{
	public Guid Id { get; set; } = Guid.NewGuid();
	
	[Required]
	[StringLength(200)]
	public string Titulo { get; set; } = default!;
	[StringLength(1000)]
	public string? Descricao { get; set; }
	[Column("Prazo", TypeName = "DateTime2")]
	public DateTime? Prazo { get; set; }
	public string? NomeResponsavel { get; set; }
	public string? Situacao { get; set; }
	public string? Observacao { get; set; }

	[Column("CriadoEm", TypeName = "DateTime2")]
	public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

	[Column("AtualizadoEm", TypeName = "DateTime2")]
	public DateTime? AtualizadoEm { get; set; }

	[Column("DeletadoEm", TypeName = "DateTime2")]
	public DateTime? DeletadoEm { get; set; }

	// Relação obrigatória com Atividade
	public Guid AtividadeId { get; set; }
	public Atividade Atividade { get; set; } = default!;
}

