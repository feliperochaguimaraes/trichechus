using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IFuncionalidadeService
	{
		Task<Result<IEnumerable<FuncionalidadeDto>>> GetAllFuncionalidadeAsync();
		Task<Result<FuncionalidadeDto>> GetFuncionalidadeByIdAsync(Guid id);
		Task<Result<FuncionalidadeDto>> CreateFuncionalidadeAsync(CreateFuncionalidadeDto dto);
		Task<Result> UpdateFuncionalidadeAsync(Guid id, UpdateFuncionalidadeDto dto);
		Task<Result> DeleteFuncionalidadeAsync(Guid id);
	}
}
