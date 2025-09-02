using Trichechus.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs;

public class BaseDadosDto
{
	public Guid Id { get; set; }
	public string Cluster { get; set; } = "";
	public string NomeBaseDados { get; set; } = "";
	public string Versao { get; set; } = "";
	public List<SoftwareDto>? Softwares { get; set; }
}
public class CreateBaseDadosDto
{
 	[Required(ErrorMessage = "O nome do Cluster é obrigatório")]
	[StringLength(100)]
	public string Cluster { get; set; } = "";

	[Required(ErrorMessage = "O nome da Base De Dados é obrigatório")]
	[StringLength(100)]
	public string NomeBaseDados { get; set; } = "";

	[Required(ErrorMessage = "A versão é obrigatório")]
	public string Versao { get; set; } = "";

	// Opcional: Lista de IDs de funcionalidades para associar ao perfil
	public List<Guid>? SoftwareIds { get; set; }
}

public class UpdateBaseDadosDto
{
	public Guid Id { get; set; } 
	public string Cluster { get; set; } = "";
	public string NomeBaseDados { get; set; } = "";
	public string Versao { get; set; } = "";
	// Opcional: Lista de IDs de Softwares para associar a uma base de dados
	public List<Guid>? SoftwareIds { get; set; }
}

// DTO para associar/desassociar Software de uma base de dados
public class AssociarSoftwareBaseDadosDto 
{
	[Required]
	public Guid SoftwareId { get; set; }
}