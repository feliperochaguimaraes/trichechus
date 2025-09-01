using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IBaseDadosService
	{
		Task<Result<BaseDadosDto>> GetByIdAsync(Guid Id);
		Task<Result<IEnumerable<BaseDadosDto>>>GetAllBaseDadosAsync();
		Task<Result<BaseDadosDto>> GetBaseDadosByIdAsync(Guid id);
		Task<Result<Guid>> CreateBaseDadosAsync(CreateBaseDadosDto dto);
		Task<Result> UpdateBaseDadosAsync(UpdateBaseDadosDto dto);
		Task<Result> DeleteBaseDadosAsync(Guid id);
		Task<Result> AddSoftBaseDadosAsync(Guid baseDadosId, Guid softwareId);
		Task<Result> RemoveSoftBaseDadosAsync(Guid baseDadosId, Guid softwareId);
	}
}

