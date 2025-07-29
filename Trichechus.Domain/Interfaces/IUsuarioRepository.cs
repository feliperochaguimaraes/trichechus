using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IUsuarioRepository
{
	Task<Usuario?> GetByIdAsync(Guid id);
	Task<Usuario?> GetByIdWithPerfilAsync(Guid id);
	Task<IEnumerable<Usuario>> GetAllAsync();
	Task<Usuario> GetByEmailAsync(string email);
	Task<bool> EmailExistsAsync(string email);
	Task AddAsync(Usuario usuario);
	Task UpdateAsync(Usuario usuario);
	Task AddPerfilAsync(Guid usuarioId, Guid perfilId);
	Task RemovePerfilAsync(Guid usuarioId, Guid perfilId);
}
