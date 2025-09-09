using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IFornecedorRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Fornecedor> GetByIdAsync(Guid id);
	Task<IEnumerable<Fornecedor>> GetAllAsync();
	Task<Fornecedor?> GetByIdWithContratosAsync(Guid id);
	Task AddAsync(Fornecedor fornecedor);
	Task UpdateAsync(Fornecedor fornecedor);
	Task DeleteAsync(Guid id);
	Task AddContratoAsync(Guid fornecedorId, Guid contratoId);
	Task RemoveContratoAsync(Guid fornecedorId, Guid contratoId);
}
