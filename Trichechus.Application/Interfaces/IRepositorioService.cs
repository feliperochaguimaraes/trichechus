using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IRepositorioService
	{
		Task<IEnumerable<RepositorioDto>> GetAllRepositorioAsync();
		Task<Result<RepositorioDto>> GetRepositorioByIdAsync(Guid id);
		Task<Result<Guid>> CreateRepositorioAsync(CreateRepositorioDto dto);
		Task<Result> UpdateRepositorioAsync(UpdateRepositorioDto dto);
		Task<Result> DeleteRepositorioAsync(Guid id);
		Task<Result> DeleteSoftRepositorioAsync(Guid id);
	}
}