using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IUsuarioLocalRepository
{
	Task<UsuarioLocal?> GetByIdAsync(Guid id);
	Task<UsuarioLocal?> GetByEmailAsync(string email);
	Task<bool> EmailExistsAsync(string email);
	Task AddAsync(UsuarioLocal usuario);
	Task UpdateAsync(UsuarioLocal usuario);
	// Adicione outros métodos conforme necessário (ex: GetAllAsync)
}
