using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IBaseDadosRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<BaseDados> GetByIdAsync(Guid id);
	Task<IEnumerable<BaseDados>> GetAllAsync();
	Task<IEnumerable<BaseDados>> GetAllBaseDadosAsync();
	Task<BaseDados?> GetByIdWithSoftwareAsync(Guid id);
	Task<BaseDados?> GetByNameAsync(string Nome);
	Task AddAsync(BaseDados baseDados);
	Task UpdateAsync(BaseDados baseDados);
	Task DeleteAsync(Guid id);
	Task AddSoftAsync(Guid baseDadosId, Guid softwareId);
	Task RemoveSoftBaseDadosAsync(Guid baseDadosId, Guid softwareId);
}

