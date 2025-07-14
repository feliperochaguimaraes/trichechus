using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateContratoDtoValidator : AbstractValidator<UpdateContratoDto>
	{
		public UpdateContratoDtoValidator()
		{
			RuleFor(dto => dto.NomeAlias)
				.NotEmpty().WithMessage("O NomeAlias do contrato é obrigatório.")
				.MaximumLength(20).WithMessage("O título não pode ter mais de 20 caracteres.");

			RuleFor(dto => dto.Objeto)
				// .NotEmpty().WithMessage("O Objeto do contrato é obrigatório.")
				.MaximumLength(4000).WithMessage("O Objeto não pode ter mais de 4000 caracteres.");

			// RuleFor(dto => dto.Inicio)
			// 	.NotEmpty().WithMessage("O Nome do fornecedor é obrigatório."); 

			// RuleFor(dto => dto.Fim)
			// 	.NotEmpty().WithMessage("A Data Nome do fornecedor é obrigatório.");

			RuleFor(dto => dto.Ativo)
				.NotEmpty().WithMessage("O campo Ativo é obrigatório.");
		}
	}
}
