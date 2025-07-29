using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Services
{
	public interface IUsuarioService
	{
		Task<Result<UsuarioDto>> GetByIdAsync(Guid id);
		Task<IEnumerable<UsuarioDto>> GetAllAsync();
		Task<Result<UsuarioDto>> CreateAsync(CreateUsuarioDto dto);
		Task<Result> UpdateAsync(Guid id, UpdateUsuarioDto dto);
		Task<Result> AddPerfilAsync(Guid usuarioId, Guid perfilId);
		Task<Result> RemovePerfilAsync(Guid usuarioId, Guid perfilId);
	}
}
