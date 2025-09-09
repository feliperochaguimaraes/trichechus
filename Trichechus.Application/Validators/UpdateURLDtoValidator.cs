using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateURLDtoValidator : AbstractValidator<UpdateUrlDto>
	{
		public UpdateURLDtoValidator()
		{
			RuleFor(dto => dto.Endereco)
				.NotEmpty().WithMessage("O Nome do fornecedor é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.Ambiente)
				.NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(10).WithMessage("O título não pode ter mais de 10 caracteres.");

			RuleFor(dto => dto.Servidor)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(30).WithMessage("O título não pode ter mais de 30 caracteres.");

			RuleFor(dto => dto.IP)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(20).WithMessage("O título não pode ter mais de 20 caracteres.");				
		}
	}
}
