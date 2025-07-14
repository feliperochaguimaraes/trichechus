using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IFuncionalidadeRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=============================================
	Task<Funcionalidade> GetByIdAsync(Guid id);
	Task<Funcionalidade> GetByNameAsync(string nome);
	Task<IEnumerable<Funcionalidade>> GetAllAsync();
	Task AddAsync(Funcionalidade funcionalidade);
	Task UpdateAsync(Funcionalidade funcionalidade);
	Task DeleteAsync(Guid id);
}

