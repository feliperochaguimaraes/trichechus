using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface ISoftwareRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Software> GetByIdAsync(Guid id);
	Task<IEnumerable<Software>> GetAllAsync();
	Task AddAsync(Software software);
	Task UpdateAsync(Software software);
	Task DeleteAsync(Guid id);
}
