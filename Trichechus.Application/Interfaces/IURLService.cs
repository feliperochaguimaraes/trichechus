using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IURLService
	{
		Task<IEnumerable<URLDto>> GetAllURLAsync();
		Task<Result<URLDto>> GetURLByIdAsync(Guid id);
		Task<Result<Guid>> CreateURLAsync(CreateURLDto dto);
		Task<Result> UpdateURLAsync(UpdateURLDto dto);
		Task<Result> DeleteURLAsync(Guid id);
		Task<Result> DeleteSoftURLAsync(Guid id);
	}
}