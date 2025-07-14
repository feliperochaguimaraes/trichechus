// Application/DTOs/UsuarioDTO.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Trichechus.Application.DTOs;

public class RegistroUsuarioDTO
{
	[Required(ErrorMessage = "O nome é obrigatório")]
	public string Nome { get; set; } = default!;
	[Required(ErrorMessage = "O email é obrigatório")]
	// [EmailAddress(ErrorMessage = "Formato de email inválido")]
	public string Email { get; set; } = default!;
	[Required(ErrorMessage = "A senha é obrigatória")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
	public string Senha { get; set; } = default!;
	[Compare("Senha", ErrorMessage = "As senhas não conferem")]
	public string ConfirmacaoSenha { get; set; } = default!;
}
public class LoginUsuarioDTO
{
	[Required(ErrorMessage = "O email é obrigatório")]
	// [EmailAddress(ErrorMessage = "Formato de email inválido")]
	public string Email { get; set; } = default!;
	[Required(ErrorMessage = "A senha é obrigatória")]
	public string Senha { get; set; } = default!;
}
public class UsuarioTokenDTO
{
	public string Token { get; set; } = default!;
	public DateTime Expiracao { get; set; }
	public string Nome { get; set; } = default!;
	public string Email { get; set; } = default!;
	public List<string> Roles { get; set; } = new List<string>();
	public List<string> Perfis { get; set; } = new List<string>();
}

public class RegistroUsuarioLocalDTO
{
	[Required(ErrorMessage = "O nome é obrigatório")]
	[StringLength(200)]
	public string Nome { get; set; } = default!;

	[Required(ErrorMessage = "O email é obrigatório")]
	[EmailAddress(ErrorMessage = "Formato de email inválido")]
	[StringLength(200)]
	public string Email { get; set; } = default!;

	[Required(ErrorMessage = "A senha é obrigatória")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
	public string Senha { get; set; } = default!;

	[Compare("Senha", ErrorMessage = "As senhas não conferem")]
	public string ConfirmacaoSenha { get; set; } = default!;

	// Opcional: Lista de Nomes ou IDs de Perfis para atribuir no registro
	public List<string>? NomesPerfis { get; set; }
}
