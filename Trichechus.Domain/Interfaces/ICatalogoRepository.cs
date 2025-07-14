using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface ICatalogoRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Catalogo> GetByIdAsync(Guid id);
	Task<IEnumerable<Catalogo>> GetAllAsync();
	Task AddAsync(Catalogo catalogo);
	Task UpdateAsync(Catalogo catalogo);
	Task DeleteAsync(Guid id);
}
