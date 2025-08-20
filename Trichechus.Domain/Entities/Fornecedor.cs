using System.ComponentModel.DataAnnotations.Schema;
namespace Trichechus.Domain.Entities;

public class Fornecedor
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Column("Nome", TypeName = "varchar(50)")]
	public string Nome { get; set; } = default!;

	[Column("Descricao", TypeName = "varchar(250)")]
	public string? Descricao { get; set; }

	[Column("CPFCNPJ", TypeName = "varchar(20)")]
	public string CPFCNPJ { get; set; } = "";

	[Column("Endereco", TypeName = "varchar(100)")]
	public string Endereco { get; set; } = "";

	[Column("Numero", TypeName = "varchar(20)")]
	public string Numero { get; set; } = "";

	[Column("Cep", TypeName = "varchar(10)")]
	public string Cep { get; set; } = "";

	[Column("Cidade", TypeName = "varchar(30)")]
	public string Cidade { get; set; } = "";

	[Column("Estado", TypeName = "varchar(20)")]
	public string Estado { get; set; } = "";

	[Column("Ativo")]
	public string Ativo { get; set; } = "Sim";

	public ICollection<Contrato> Contrato { get; set; } = new List<Contrato>();
}