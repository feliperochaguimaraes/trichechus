// Application/Validators/LoginUsuarioDTOValidator.cs
using FluentValidation;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Validators;

public class LoginUsuarioDTOValidator : AbstractValidator<LoginUsuarioDTO>
{
	public LoginUsuarioDTOValidator()
	{
		RuleFor(u => u.Email)
			.NotEmpty().WithMessage("O email é obrigatório");
			// .EmailAddress().WithMessage("Formato de email inválido");

		RuleFor(u => u.Senha)
			.NotEmpty().WithMessage("A senha é obrigatória");
	}
}