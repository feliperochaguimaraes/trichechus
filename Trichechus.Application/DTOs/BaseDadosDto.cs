namespace Trichechus.Application.DTOs;

public class BaseDadosDto
{
	public Guid Id { get; set; }
	public required string Cluster { get; set; }
	public required string NomeBaseDados { get; set; }
	public required string Versao { get; set; }
}
public class CreateBaseDadosDto
{
	public required string Cluster { get; set; }
	public required string NomeBaseDados { get; set; }
	public required string Versao { get; set; }
}

public class UpdateBaseDadosDto
{
	public Guid Id { get; set; }
	public required string Cluster { get; set; }
	public required string NomeBaseDados { get; set; }
	public required string Versao { get; set; }
}