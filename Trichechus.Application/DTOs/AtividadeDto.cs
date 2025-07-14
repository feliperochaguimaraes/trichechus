using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs;

public class AtividadeDto
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = default!;
	public string? Descricao { get; set; }
	public string? Situacao { get; set; }
	public string? NomeResponsavel { get; set; }
	public DateTime Prazo { get; set; }
	public string? NomeEquipeResponsavel { get; set; }
	public string? TipoEntrada { get; set; }
	public DateTime CriadoEm { get; set; }
	public DateTime? AtualizadoEm { get; set; }

	public ICollection<TarefaDto>? Tarefas { get; set; }
}

public class CreateAtividadeDto
{
	/// <summary>
	/// Título da atividade
	/// </summary>
	/// <example>Implementar API REST</example>
	[Required(ErrorMessage = "O título é obrigatório")]
	[StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres")]
	public string Titulo { get; set; } = default!;

	/// <summary>
	/// Descrição detalhada da atividade
	/// </summary>
	/// <example>Implementar uma API REST usando ASP.NET Core e Entity Framework Core</example>
	[StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres")]
	public string? Descricao { get; set; }

	/// <summary>
	/// Situação atual da atividade
	/// </summary>
	/// <example>Em andamento</example>
	public string? Situacao { get; set; }

	/// <summary>
	/// Nome do responsável pela atividade
	/// </summary>
	/// <example>João Silva</example>
	public string? NomeResponsavel { get; set; }

	/// <summary>
	/// Prazo para conclusão da atividade
	/// </summary>
	/// <example>2023-12-31T23:59:59</example>
	[Required(ErrorMessage = "O prazo é obrigatório")]
	public DateTime Prazo { get; set; }

	/// <summary>
	/// Nome da equipe responsável pela atividade
	/// </summary>
	/// <example>Equipe de Desenvolvimento</example>
	public string? NomeEquipeResponsavel { get; set; }

	/// <summary>
	/// Tipo de entrada da atividade
	/// </summary>
	/// <example>Demanda</example>
	public string? TipoEntrada { get; set; }
}

public class UpdateAtividadeDto
{
	public Guid Id { get; set; }
	public string Titulo { get; set; } = default!;
	public string? Descricao { get; set; }
	public string? Situacao { get; set; }
	public string? NomeResponsavel { get; set; }
	public DateTime Prazo { get; set; }
	public string? NomeEquipeResponsavel { get; set; }
	public string? TipoEntrada { get; set; }
}

public class DeleteSoftAtividadeDto
{
	public Guid Id { get; set; }
	public DateTime DeletadoEm { get; set; } 
}