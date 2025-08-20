namespace Trichechus.Application.DTOs;

public class ContratoDto
{
	public Guid Id { get; set; }
	public string NomeAlias { get; set; } = "";
	public string Numero { get; set; } = string.Empty; 
	public string Objeto { get; set; } = "";
	public string Ativo { get; set; } = "";
	public DateTime Inicio { get; set; }
	public DateTime Fim { get; set; }
	public string AreaGestora { get; set; } = "";
	public string Gerencia { get; set; } = "";
}
public class CreateContratoDto
{
	public string NomeAlias { get; set; } = "";
	public string Numero { get; set; } = string.Empty; 
	public string Objeto { get; set; } = "";
	public string Ativo { get; set; } = "";
	public DateTime Inicio { get; set; }
	public DateTime Fim { get; set; }
	public string AreaGestora { get; set; } = "";
	public string Gerencia { get; set; } = "";

}

public class UpdateContratoDto
{
	public Guid Id { get; set; }
	public string NomeAlias { get; set; } = "";
	public string Numero { get; set; } = string.Empty; 
	public string Objeto { get; set; } = "";
	public string Ativo { get; set; } = "";
	public DateTime Inicio { get; set; }
	public DateTime Fim { get; set; }
	public string AreaGestora { get; set; } = "";
	public string Gerencia { get; set; } = "";
}