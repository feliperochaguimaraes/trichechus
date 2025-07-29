using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trichechus.Domain.Entities;

public class Atividade
{
	public Guid Id { get; set; } = Guid.NewGuid();
	
	[Required]
	[StringLength(200)]
	public string Titulo { get; set; } = default!;
	[StringLength(1000)]

	public string? Descricao { get; set; }
	public string? Situacao { get; set; } // Enum opcional no futuro
	public string? NomeResponsavel { get; set; }

	[Column("Prazo", TypeName = "DateTime2")]
	public DateTime Prazo { get; set; }
	public string? NomeEquipeResponsavel { get; set; }

	[Column("CriadoEm", TypeName = "DateTime2")]
	public DateTime CriadoEm { get; set; } = DateTime.Now;

	[Column("AtualizadoEm", TypeName = "DateTime2")]
	public DateTime? AtualizadoEm { get; set; }

	[Column("DeletadoEm", TypeName = "DateTime2")]
	public DateTime? DeletadoEm { get; set; }
	public string? TipoEntrada { get; set; }

	// Navegação para as tarefas
	public ICollection<Tarefa> Tarefa { get; set; } = new List<Tarefa>();
}