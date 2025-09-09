using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IURLService
	{
		Task<Result<UrlDto>> GetByIdAsync(Guid id);
		Task<Result<IEnumerable<UrlDto>>> GetAllUrlAsync();
		Task<Result<UrlDto>> GetUrlByIdAsync(Guid id);
		Task<Result<Guid>> CreateUrlAsync(CreateUrlDto dto);
		Task<Result> UpdateUrlAsync(UpdateUrlDto dto);
		Task<Result> DeleteUrlAsync(Guid id);
		Task<Result> AddSoftUrlAsync(Guid urlId, Guid softwareId);
		Task<Result> DeleteSoftUrlAsync(Guid urlId, Guid softwareId);
	}
}