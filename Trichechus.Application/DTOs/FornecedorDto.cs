namespace Trichechus.Application.DTOs;

public class FornecedorDto
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = "";
	public string CPFCNPJ { get; set; } = "";
	public string Endereco { get; set; } = "";
	public string Numero { get; set; } = "";
	public string Cep { get; set; } = "";
	public string Cidade { get; set; } = "";
	public string Estado { get; set; } = "";
	public string Ativo { get; set; } = ""; 
}

public class CreateFornecedorDto
{
	public string Nome { get; set; } = "";
	public string CPFCNPJ { get; set; } = "";
	public string Endereco { get; set; } = "";
	public string Numero { get; set; } = "";
	public string Cep { get; set; } = "";
	public string Cidade { get; set; } = "";
	public string Estado { get; set; } = "";
	public string Ativo { get; set; } = "";
}

public class UpdateFornecedorDto
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = "";
	public string CPFCNPJ { get; set; } = "";
	public string Endereco { get; set; } = "";
	public string Numero { get; set; } = "";
	public string Cep { get; set; } = "";
	public string Cidade { get; set; } = "";
	public string Estado { get; set; } = "";
	public string Ativo { get; set; } = "";
}