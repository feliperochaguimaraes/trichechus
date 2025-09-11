using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs;

public class GerenciaDto
{
	public Guid Id { get; set; }
	public string? NomeGerencia { get; set; }
	public string? Superintendencia { get; set; }
}

public class CreateGerenciaDto
{
	[Required(ErrorMessage = "O campo Gerencia é obrigatório")]
	[StringLength(50, MinimumLength = 5 , ErrorMessage = "O campo {0} deve ter pelo menos {2} caracteres e não mais que {1} caracteres.")]
	public string NomeGerencia { get; set; } = "";

	[Required(ErrorMessage = "O campo Superintendencia é obrigatório")]
	[StringLength(50, MinimumLength = 5 , ErrorMessage = "O campo {0} deve ter pelo menos {2} caracteres e não mais que {1} caracteres.")]
	public string Superintendencia { get; set; } = "";
}

public class UpdateGerenciaDto
{
	public Guid Id { get; set; }
	
	[Required(ErrorMessage = "O campo Gerencia é obrigatório")]
	[StringLength(50, MinimumLength = 5 , ErrorMessage = "O campo {0} deve ter pelo menos {2} caracteres e não mais que {1} caracteres.")]
	public string NomeGerencia { get; set; } = "";

	[Required(ErrorMessage = "O campo Superintendencia é obrigatório")]
	[StringLength(50, MinimumLength = 5 , ErrorMessage = "O campo {0} deve ter pelo menos {2} caracteres e não mais que {1} caracteres.")]
	public string Superintendencia { get; set; } = "";
}

// public class DeleteSoftGerenciaDto
// {
// 	public Guid Id { get; set; }
// }