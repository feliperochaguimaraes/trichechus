using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class CreateSoftwareDtoValidator : AbstractValidator<CreateSoftwareDto>
	{
		public CreateSoftwareDtoValidator()
		{
			RuleFor(dto => dto.ProdutoSoftware)
				.NotEmpty().WithMessage("O Campo ProdutoSoftware é obrigatório.")
				.MaximumLength(50).WithMessage("O título não pode ter mais de 50 caracteres.");

			RuleFor(dto => dto.Nome)
				.NotEmpty().WithMessage("O Campo Nome é obrigatório.")
				.MaximumLength(100).WithMessage("O Campo Nome não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.Situacao)
				.NotEmpty().WithMessage("O Campo Situação é obrigatório.")
				.MaximumLength(20).WithMessage("O Campo Situação não pode ter mais de 20 caracteres.");

			// RuleFor(dto => dto.Descricao)
			// 	.NotEmpty().WithMessage("O Nome do fornecedor é obrigatório.")
			// 	.MaximumLength(50).WithMessage("O título não pode ter mais de 200 caracteres.");

			RuleFor(dto => dto.Segmento)
				.MaximumLength(20).WithMessage("O Campo Segmento não pode ter mais de 20 caracteres.");

			RuleFor(dto => dto.Tipo)
				.NotEmpty().WithMessage("O Campo Tipo é obrigatório.")
				.MaximumLength(30).WithMessage("O Campo Tipo não pode ter mais de 30 caracteres.");

			RuleFor(dto => dto.LicencaSoftware)
				.NotEmpty().WithMessage("O Campo LicencaSoftware é obrigatório.")
				.MaximumLength(3).WithMessage("O Campo LicencaSoftware não pode ter mais de 3 caracteres.");

			// RuleFor(dto => dto.Tecnologia)
			// 	.NotEmpty().WithMessage("O Campo Tecnologia é obrigatório.")
			// 	.MaximumLength(50).WithMessage("O título não pode ter mais de 200 caracteres.");

			// RuleFor(dto => dto.Descontinuado)
			// .NotEmpty().WithMessage("O Campo Tipo  é obrigatório.")
			// .MaximumLength(50).WithMessage("O título não pode ter mais de 200 caracteres.");
				
			RuleFor(dto => dto.EntrarCatalogo)
				.NotEmpty().WithMessage("O Campo EntrarCatalogo é obrigatório.")
				.MaximumLength(3).WithMessage("O Campo EntrarCatalogo não pode ter mais de 3 caracteres.");
		}
	}
}
