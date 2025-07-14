using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface ISoftwareService
	{
		Task<IEnumerable<SoftwareDto>> GetAllSoftwareAsync();
		Task<Result<SoftwareDto>> GetSoftwareByIdAsync(Guid id);
		Task<Result<Guid>> CreateSoftwareAsync(CreateSoftwareDto dto);
		Task<Result> UpdateSoftwareAsync(UpdateSoftwareDto dto);
		Task<Result> DeleteSoftwareAsync(Guid id);
		Task<Result> DeleteSoftSoftwareAsync(Guid id);
	}
}
