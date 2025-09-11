using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateGerenciaDtoValidator : AbstractValidator<UpdateGerenciaDto>
	{
		public UpdateGerenciaDtoValidator()
		{
			RuleFor(dto => dto.NomeGerencia)
				.NotEmpty().WithMessage("O título da atividade é obrigatório.")
				.MaximumLength(50).WithMessage("O Campo Gerencia não pode ter mais de 50 caracteres.");

			RuleFor(dto => dto.Superintendencia)
				.MaximumLength(50).WithMessage("O campo Superintendencia não pode ter mais de 50 caracteres.");
		}
	}
}
