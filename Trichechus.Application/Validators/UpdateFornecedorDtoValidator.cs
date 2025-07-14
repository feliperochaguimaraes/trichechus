using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateFornecedorDtoValidator : AbstractValidator<UpdateFornecedorDto>
	{
		public UpdateFornecedorDtoValidator()
		{
			RuleFor(dto => dto.Nome)
				.NotEmpty().WithMessage("O Nome do fornecedor é obrigatório.")
				.MaximumLength(50).WithMessage("O título não pode ter mais de 200 caracteres.");

			RuleFor(dto => dto.Ativo)
				.NotEmpty().WithMessage("O campo Ativo é obrigatório.");
		}
	}
}
