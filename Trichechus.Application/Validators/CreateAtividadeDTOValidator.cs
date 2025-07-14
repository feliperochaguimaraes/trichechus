using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators
{
	public class CreateAtividadeDtoValidator : AbstractValidator<CreateAtividadeDto>
	{
		public CreateAtividadeDtoValidator()
		{
			RuleFor(dto => dto.Titulo)
				.NotEmpty().WithMessage("O título da atividade é obrigatório.")
				.MaximumLength(200).WithMessage("O título não pode ter mais de 200 caracteres.");

			RuleFor(dto => dto.Descricao)
				.MaximumLength(1000).WithMessage("A descrição não pode ter mais de 1000 caracteres.");

			RuleFor(dto => dto.Prazo)
				.NotEmpty().WithMessage("O prazo da atividade é obrigatório.")
				.GreaterThan(DateTime.Now).WithMessage("O prazo deve ser uma data futura.");

			RuleFor(dto => dto.NomeResponsavel)
				.MaximumLength(100).WithMessage("O nome do responsável não pode ter mais de 100 caracteres.");

			RuleFor(dto => dto.NomeEquipeResponsavel)
				.MaximumLength(100).WithMessage("O nome da equipe responsável não pode ter mais de 100 caracteres.");
		}
	}
}
