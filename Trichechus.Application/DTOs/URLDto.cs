using Trichechus.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs;

public class UrlDto
{
	public Guid Id { get; set; }
	public required string Endereco { get; set; }
	public required string Ambiente { get; set; } //DEV|PROD
	public required string Servidor { get; set; }
	public required string IP { get; set; }
	public List<SoftwareDto>? Softwares { get; set; }
}
public class CreateUrlDto
{
	public required string Endereco { get; set; }
	public required string Ambiente { get; set; } //DEV|PROD
	public required string Servidor { get; set; }
	public required string IP { get; set; }

	// Opcional: Lista de IDs de funcionalidades para associar ao perfil
	public List<Guid>? SoftwareIds { get; set; }
}

public class UpdateUrlDto
{
	public Guid Id { get; set; }
	public required string Endereco { get; set; }
	public required string Ambiente { get; set; } //DEV|PROD
	public required string Servidor { get; set; }
	public required string IP { get; set; }
	public List<Guid>? SoftwareIds { get; set; }
}

// DTO para associar/desassociar Software de uma url
public class AssociarSoftwareUrlDto
{
	[Required]
	public Guid SoftwareId { get; set; }
}