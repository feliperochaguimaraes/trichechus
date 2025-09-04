using Trichechus.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs;

public class CatalogoDto
{
	public Guid Id { get; set; }
	public string? HelixId { get; set; } = null;
	public string? HelixEquipe { get; set; } = null;
	public string? HelixService { get; set; } = null;
	public string? HelixCategoria { get; set; } = null;
	public string? HelixSubcategoria { get; set; } = null;
	public string CatalogoEquipe { get; set; } = "";
	public string Ativo { get; set; } = ""; //Sim|Não
	public string? Observacao { get; set; } = null;
	public List<SoftwareDto>? Softwares { get; set; }
}

public class CreateCatalogoDto
{
	public string? HelixId { get; set; } = null;
	public string? HelixEquipe { get; set; } = null;
	public string? HelixService { get; set; } = null;
	public string? HelixCategoria { get; set; } = null;
	public string? HelixSubcategoria { get; set; } = null;
	public string CatalogoEquipe { get; set; } = "";
	public string Ativo { get; set; } = ""; //Sim|Não
	public string? Observacao { get; set; } = null;
	// Opcional: Lista de IDs de funcionalidades para associar ao perfil
	public List<Guid>? SoftwareIds { get; set; }
}


public class UpdateCatalogoDto
{
	public Guid Id { get; set; }
	public string? HelixId { get; set; } = null;
	public string? HelixEquipe { get; set; } = null;
	public string? HelixService { get; set; } = null;
	public string? HelixCategoria { get; set; } = null;
	public string? HelixSubcategoria { get; set; } = null;
	public string CatalogoEquipe { get; set; } = "";
	public string Ativo { get; set; } = ""; //Sim|Não
	public string? Observacao { get; set; } = null;
	// Opcional: Lista de IDs de Softwares para associar a uma base de dados
	public List<Guid>? SoftwareIds { get; set; }
}

// DTO para associar/desassociar Software de uma base de dados
public class AssociarSoftwareCatalogoDto
{
	[Required]
	public Guid SoftwareId { get; set; }
}