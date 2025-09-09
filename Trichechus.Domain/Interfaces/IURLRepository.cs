using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IURLRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<URL> GetByIdAsync(Guid id);
	Task<IEnumerable<URL>> GetAllAsync();
	Task<IEnumerable<URL>> GetAllUrlAsync();
	Task<URL?> GetByIdWithSoftwareAsync(Guid id);
	Task AddAsync(URL url);
	Task UpdateAsync(URL url);
	Task DeleteAsync(Guid id);
	Task AddSoftUrlAsync(Guid urlId, Guid softwareId);
	Task DeleteSoftUrlAsync(Guid urlId, Guid softwareId);
}

