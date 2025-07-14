using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IPerfilRepository
{
	Task<Perfil?> GetByIdAsync(Guid id);
	Task<Perfil?> GetByIdWithFuncionalidadesAsync(Guid id);
	Task<Perfil?> GetByNameAsync(string nome);
	Task<IEnumerable<Perfil>> GetAllAsync();
	Task AddAsync(Perfil perfil);
	Task UpdateAsync(Perfil perfil);
	Task DeleteAsync(Guid id);
	Task AddFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId);
	Task RemoveFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId);
}
