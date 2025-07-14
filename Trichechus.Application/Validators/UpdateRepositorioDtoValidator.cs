using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateRepositorioDtoValidator : AbstractValidator<UpdateRepositorioDto>
	{
		public UpdateRepositorioDtoValidator()
		{
			RuleFor(dto => dto.Endereco)
				.NotEmpty().WithMessage("O Endereço é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.Tipo)
				.NotEmpty().WithMessage("O campo Tipo é obrigatório.")
				.MaximumLength(20).WithMessage("O título não pode ter mais de 20 caracteres.");
		}
	}
}
