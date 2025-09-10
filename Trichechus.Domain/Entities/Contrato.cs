using System.ComponentModel.DataAnnotations.Schema;
namespace Trichechus.Domain.Entities;

public class Contrato
{
	public Guid Id { get; set; } = Guid.NewGuid();
	[Column("NomeAlias", TypeName = "varchar(20)")]
	public string NomeAlias { get; set; } = "";
	[Column("Numero", TypeName = "varchar(12)")]
	public string Numero { get; set; } = string.Empty;
	
	[Column("Objeto", TypeName = "Text")]
	public string Objeto { get; set; } = "";
	public string Ativo { get; set; } = "";

	[Column("Inicio", TypeName = "DateTime2")]
	public DateTime Inicio { get; set; }

	[Column("Fim", TypeName = "DateTime2")]
	public DateTime Fim { get; set; }

	[Column("AreaGestora", TypeName = "varchar(10)")]
	public string AreaGestora { get; set; } = "";

	// Relação obrigatória com Atividade
	public Guid GerenciaId { get; set; }
	public Gerencia Gerencia { get; set; } = default!;

	public ICollection<Fornecedor> Fornecedor { get; set; } = new List<Fornecedor>();
	public ICollection<Software> Software { get; set; } = new List<Software>();

    // public static IEnumerable<Trichechus.Application.DTOs.ContratoDto> Select(Func<object, Trichechus.Application.DTOs.ContratoDto> value)
    // {
    //     throw new NotImplementedException();
    // }
}