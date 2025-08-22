using System.ComponentModel.DataAnnotations;

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
	public List<ContratoDto>? Contratos { get; set; }
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
	public List<Guid>? ContratoIds { get; set; }
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
	public List<Guid>? ContratoIds { get; set; }
}


// DTO para associar/desassociar fornecedor de contrato
public class AssociarContratoFornecedorDto
{
	[Required]
	public Guid ContratoId { get; set; }
}