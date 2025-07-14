using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateTarefaDtoValidator : AbstractValidator<UpdateTarefaDto>
	{
		public UpdateTarefaDtoValidator()
		{
			RuleFor(dto => dto.Id)
				.NotEmpty().WithMessage("O ID da tarefa é obrigatório.");

			RuleFor(dto => dto.Titulo)
				.NotEmpty().WithMessage("O título da tarefa é obrigatório.")
				.MaximumLength(200).WithMessage("O título não pode ter mais de 200 caracteres.");

			RuleFor(dto => dto.Descricao)
				.MaximumLength(1000).WithMessage("A descrição não pode ter mais de 1000 caracteres.");

			RuleFor(dto => dto.Prazo)
				.GreaterThan(DateTime.Now).When(dto => dto.Prazo.HasValue).WithMessage("O prazo deve ser uma data futura.");

			RuleFor(dto => dto.NomeResponsavel)
				.MaximumLength(100).WithMessage("O nome do responsável não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.AtividadeId)
				.NotEmpty().WithMessage("A tarefa deve estar associada a uma atividade.");
		}
	}
}
