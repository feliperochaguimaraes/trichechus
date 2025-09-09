using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IContratoRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=============================================
	Task<Contrato> GetByIdAsync(Guid id);
	Task<IEnumerable<Contrato>> GetAllContratoAsync();
	Task<Contrato?> GetByIdWithFornecedorAsync(Guid id);
	Task<Contrato?> GetByNameAsync(string Nome);
	Task AddAsync(Contrato contrato);
	Task UpdateAsync(Contrato contrato);
	Task DeleteAsync(Guid id);
	Task AddFornecedorAsync(Guid contratoId, Guid fornecedorId);
	Task RemoveFornecedorAsync(Guid contratoId, Guid fornecedorId);
}