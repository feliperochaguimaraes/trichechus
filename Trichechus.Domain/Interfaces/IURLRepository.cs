using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IURLRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<URL> GetByIdAsync(Guid id);
	Task<IEnumerable<URL>> GetAllAsync();
	Task AddAsync(URL url);
	Task UpdateAsync(URL url);
	Task DeleteAsync(Guid id);
}
