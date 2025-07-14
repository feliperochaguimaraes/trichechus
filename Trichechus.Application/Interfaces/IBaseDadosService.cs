using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IBaseDadosService
	{
		Task<IEnumerable<BaseDadosDto>> GetAllBaseDadosAsync();
		Task<Result<BaseDadosDto>> GetBaseDadosByIdAsync(Guid id);
		Task<Result<Guid>> CreateBaseDadosAsync(CreateBaseDadosDto dto);
		Task<Result> UpdateBaseDadosAsync(UpdateBaseDadosDto dto);
		Task<Result> DeleteBaseDadosAsync(Guid id);
		Task<Result> DeleteSoftBaseDadosAsync(Guid id);
	}
}