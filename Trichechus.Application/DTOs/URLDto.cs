namespace Trichechus.Application.DTOs;

public class URLDto
{
	public Guid Id { get; set; }
	public required string Endereco { get; set; }
	public required string Ambiente { get; set; } //DEV|PROD
	public required string Servidor { get; set; }
	public required string IP { get; set; }
}
public class CreateURLDto
{
	public required string Endereco { get; set; }
	public required string Ambiente { get; set; } //DEV|PROD
	public required string Servidor { get; set; }
	public required string IP { get; set; }
}

public class UpdateURLDto
{
	public Guid Id { get; set; }
	public required string Endereco { get; set; }
	public required string Ambiente { get; set; } //DEV|PROD
	public required string Servidor { get; set; }
	public required string IP { get; set; }
}