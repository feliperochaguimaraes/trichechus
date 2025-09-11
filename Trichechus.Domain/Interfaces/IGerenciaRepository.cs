using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IGerenciaRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=============================================
	Task<Gerencia> GetByIdAsync(Guid id);
	Task<IEnumerable<Gerencia>> GetAllAsync();
	Task AddAsync(Gerencia gerencia);
	Task UpdateAsync(Gerencia gerencia);
	Task DeleteAsync(Guid id);
}
