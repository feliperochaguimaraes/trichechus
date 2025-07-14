namespace Trichechus.Application.DTOs;

public class SoftwareDto
{
	public Guid Id { get; set; }
	public string ProdutoSoftware { get; set; } = "";
	public required string Nome { get; set; }
	public required string Situacao { get; set; }
	public string? Descricao { get; set; } = null;
	public string Segmento { get; set; } = "";
	public string Tipo { get; set; } = ""; //Desktop|Web|API|WEBSERVICE|Serviço Windows|Serviço
	public string LicencaSoftware { get; set; } = ""; //Sim|Não
	public string Tecnologia { get; set; } = "";
	public DateTime Descontinuado { get; set; }
	public string EntrarCatalogo { get; set; } = ""; //Sim|Não
}
public class CreateSoftwareDto
{
	public string ProdutoSoftware { get; set; } = "";
	public required string Nome { get; set; }
	public required string Situacao { get; set; }
	public string? Descricao { get; set; } = null;
	public string Segmento { get; set; } = "";
	public string Tipo { get; set; } = ""; //Desktop|Web|API|WEBSERVICE|Serviço Windows|Serviço
	public string LicencaSoftware { get; set; } = ""; //Sim|Não
	public string Tecnologia { get; set; } = "";
	public DateTime Descontinuado { get; set; }
	public string EntrarCatalogo { get; set; } = ""; //Sim|Não
}

public class UpdateSoftwareDto
{
	public Guid Id { get; set; }
	public string ProdutoSoftware { get; set; } = "";
	public required string Nome { get; set; }
	public required string Situacao { get; set; }
	public string? Descricao { get; set; } = null;
	public string Segmento { get; set; } = "";
	public string Tipo { get; set; } = ""; //Desktop|Web|API|WEBSERVICE|Serviço Windows|Serviço
	public string LicencaSoftware { get; set; } = ""; //Sim|Não
	public string Tecnologia { get; set; } = "";
	public DateTime Descontinuado { get; set; }
	public string EntrarCatalogo { get; set; } = ""; //Sim|Não
}