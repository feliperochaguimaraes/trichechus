using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class UpdateCatalogoDtoValidator : AbstractValidator<UpdateCatalogoDto>
	{
		public UpdateCatalogoDtoValidator()
		{
			RuleFor(dto => dto.HelixId)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(20).WithMessage("O título não pode ter mais de 20 caracteres.");

			RuleFor(dto => dto.HelixEquipe)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.HelixService)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");
				
			RuleFor(dto => dto.HelixCategoria)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.HelixSubcategoria)
				// .NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.CatalogoEquipe)
				.NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(100).WithMessage("O título não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.Ativo)
				.NotEmpty().WithMessage("O campo Ativo é obrigatório.")
				.MaximumLength(3).WithMessage("O título não pode ter mais de 3 caracteres.");

			// RuleFor(dto => dto.Observacao)
			// 	.NotEmpty().WithMessage("O campo Ativo é obrigatório.")
			// 	.MaximumLength(20).WithMessage("O título não pode ter mais de 20 caracteres.");
		}
	}
}
