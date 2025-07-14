using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IBaseDadosRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<BaseDados> GetByIdAsync(Guid id);
	Task<IEnumerable<BaseDados>> GetAllAsync();
	Task AddAsync(BaseDados baseDados);
	Task UpdateAsync(BaseDados baseDados);
	Task DeleteAsync(Guid id);
}
