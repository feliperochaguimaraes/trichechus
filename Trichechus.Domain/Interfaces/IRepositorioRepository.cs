using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IRepositorioRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Repositorio> GetByIdAsync(Guid id);
	Task<IEnumerable<Repositorio>> GetAllAsync();
	Task AddAsync(Repositorio repositorio);
	Task UpdateAsync(Repositorio repositorio);
	Task DeleteAsync(Guid id);
}
