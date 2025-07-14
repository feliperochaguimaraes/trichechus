using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Services
{
	public interface IFuncionalidadeService
	{
		Task<Result<FuncionalidadeDto>> GetByIdAsync(Guid id);
		Task<Result<IEnumerable<FuncionalidadeDto>>> GetAllAsync();
		Task<Result<FuncionalidadeDto>> CreateAsync(CreateFuncionalidadeDto dto);
		Task<Result> UpdateAsync(Guid id, UpdateFuncionalidadeDto dto);
		Task<Result> DeleteAsync(Guid id);
	}
}
