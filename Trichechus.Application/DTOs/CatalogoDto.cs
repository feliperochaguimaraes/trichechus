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
}