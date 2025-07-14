using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class CreateBaseDadosDtoValidator : AbstractValidator<CreateBaseDadosDto>
	{
		public CreateBaseDadosDtoValidator()
		{
			RuleFor(dto => dto.Cluster)
				.NotEmpty().WithMessage("O Nome do Cluster é obrigatório.")
				.MaximumLength(50).WithMessage("O título não pode ter mais de 200 caracteres.");

			RuleFor(dto => dto.NomeBaseDados)
				.NotEmpty().WithMessage("O campo NomeBaseDados é obrigatório.");

			RuleFor(dto => dto.Versao)
				.NotEmpty().WithMessage("O campo Versao é obrigatório.");
		}
	}
}
