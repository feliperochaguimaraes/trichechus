using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface ICatalogoService
	{
		Task<IEnumerable<CatalogoDto>> GetAllCatalogoAsync();
		Task<Result<CatalogoDto>> GetCatalogoByIdAsync(Guid id);
		Task<Result<Guid>> CreateCatalogoAsync(CreateCatalogoDto dto);
		Task<Result> UpdateCatalogoAsync(UpdateCatalogoDto dto);
		Task<Result> DeleteCatalogoAsync(Guid id);
		Task<Result> DeleteSoftCatalogoAsync(Guid id);
	}
}