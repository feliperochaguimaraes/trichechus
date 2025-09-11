using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class CreateGerenciaDtoValidator : AbstractValidator<CreateGerenciaDto>
	{
		public CreateGerenciaDtoValidator()
		{
			RuleFor(dto => dto.NomeGerencia)
				.NotEmpty().WithMessage("O título da atividade é obrigatório.")
				.MaximumLength(50).WithMessage("O título não pode ter mais de 200 caracteres.");

			RuleFor(dto => dto.Superintendencia)
				.MaximumLength(50).WithMessage("A descrição não pode ter mais de 1000 caracteres.");
		}
	}
}
