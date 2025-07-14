using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IFornecedorRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Fornecedor> GetByIdAsync(Guid id);
	Task<IEnumerable<Fornecedor>> GetAllAsync();
	Task AddAsync(Fornecedor fornecedor);
	Task UpdateAsync(Fornecedor fornecedor);
	Task DeleteAsync(Guid id);
}
