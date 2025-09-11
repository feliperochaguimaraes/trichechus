using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IGerenciaService
	{
		Task<IEnumerable<GerenciaDto>> GetAllAsync();
		Task<Result<GerenciaDto>> GetByIdAsync(Guid id);
		Task<Result<Guid>> CreateAsync(CreateGerenciaDto dto);
		Task<Result> UpdateAsync(UpdateGerenciaDto dto);
		Task<Result> DeleteAsync(Guid id);
	}
}
