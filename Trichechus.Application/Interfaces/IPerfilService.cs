using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Services
{
	public interface IPerfilService
	{
		Task<Result<PerfilDto>> GetByIdAsync(Guid id);
		Task<Result<IEnumerable<PerfilDto>>> GetAllAsync();
		Task<Result<PerfilDto>> CreateAsync(CreatePerfilDto dto);
		Task<Result> UpdateAsync(Guid id, UpdatePerfilDto dto);
		Task<Result> DeleteAsync(Guid id);
		Task<Result> AddFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId);
		Task<Result> RemoveFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId);
	}
}
