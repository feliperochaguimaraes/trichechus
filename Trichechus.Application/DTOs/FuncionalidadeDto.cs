using System;
using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs
{
	// DTO para exibir funcionalidades
	public class FuncionalidadeDto
	{
		public Guid Id { get; set; }
		public string Nome { get; set; } = default!;
		public string? Descricao { get; set; }
	}

	// DTO para criar/atualizar funcionalidades
	public class CreateFuncionalidadeDto
	{
		[Required(ErrorMessage = "O nome da funcionalidade é obrigatório")]
		[StringLength(100)]
		public string Nome { get; set; } = default!;

		[StringLength(500)]
		public string? Descricao { get; set; }
	}

	public class UpdateFuncionalidadeDto
	{
		public Guid Id { get; set; }
		
		[Required(ErrorMessage = "O nome da funcionalidade é obrigatório")]
		[StringLength(100)]
		public string Nome { get; set; } = default!;

		[StringLength(500)]
		public string? Descricao { get; set; }
	}
}
