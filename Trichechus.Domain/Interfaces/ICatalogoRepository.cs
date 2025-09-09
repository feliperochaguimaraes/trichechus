using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface ICatalogoRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Catalogo> GetByIdAsync(Guid id);
	Task<IEnumerable<Catalogo>> GetAllAsync();
	Task<IEnumerable<Catalogo>> GetAllCatalogoAsync();
	Task<Catalogo?> GetByIdWithSoftwareAsync(Guid id);
	Task AddAsync(Catalogo catalogo);
	Task UpdateAsync(Catalogo catalogo);
	Task DeleteAsync(Guid id);
	Task AddSoftCatalogoAsync(Guid catalogoId, Guid softwareId);
	Task DeleteSoftCatalogoAsync(Guid catalogoId, Guid softwareId);
}

    