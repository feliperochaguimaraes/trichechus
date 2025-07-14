namespace Trichechus.Application.DTOs;

public class TarefaDto
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = default!;
	public string? Descricao { get; set; }
	public DateTime? Prazo { get; set; }
	public string? NomeResponsavel { get; set; }
	public string? Situacao { get; set; }
	public string? Observacao { get; set; }
	public DateTime CriadoEm { get; set; }
	public DateTime? AtualizadoEm { get; set; }
	public Guid AtividadeId { get; set; }
}

public class CreateTarefaDto
{
	public string Titulo { get; set; } = default!;
	public string? Descricao { get; set; }
	public DateTime? Prazo { get; set; }
	public string? NomeResponsavel { get; set; }
	public string? Situacao { get; set; }
	public string? Observacao { get; set; }
	public Guid AtividadeId { get; set; }
}

public class UpdateTarefaDto
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = default!;
	public string? Descricao { get; set; }
	public DateTime? Prazo { get; set; }
	public string? NomeResponsavel { get; set; }
	public string? Situacao { get; set; }
	public string? Observacao { get; set; }
	public Guid AtividadeId { get; set; }
}