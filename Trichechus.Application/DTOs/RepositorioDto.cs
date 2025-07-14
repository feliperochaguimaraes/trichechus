namespace Trichechus.Application.DTOs;

public class RepositorioDto
{
	public Guid Id { get; set; }
	public required string Endereco { get; set; }
	public required string Tipo { get; set; } //SVN|GIT
}
public class CreateRepositorioDto
{
	public required string Endereco { get; set; }
	public required string Tipo { get; set; } //SVN|GIT
}

public class UpdateRepositorioDto
{
	public Guid Id { get; set; }
	public required string Endereco { get; set; }
	public required string Tipo { get; set; } //SVN|GIT
}