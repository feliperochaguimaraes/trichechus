// Application/Validators/RegistroUsuarioDTOValidator.cs
using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators;

public class RegistroUsuarioDTOValidator : AbstractValidator<RegistroUsuarioDTO>
{
	public RegistroUsuarioDTOValidator()
	{
		RuleFor(u => u.Nome)
			.NotEmpty().WithMessage("O nome é obrigatório")
			.MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres");

		RuleFor(u => u.Email)
			.NotEmpty().WithMessage("O email é obrigatório")
			.EmailAddress().WithMessage("Formato de email inválido");

		RuleFor(u => u.Senha)
			.NotEmpty().WithMessage("A senha é obrigatória")
			.MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres")
			.MaximumLength(100).WithMessage("A senha não pode ter mais de 100 caracteres");

		RuleFor(u => u.ConfirmacaoSenha)
			.Equal(u => u.Senha).WithMessage("As senhas não conferem");
	}
}
