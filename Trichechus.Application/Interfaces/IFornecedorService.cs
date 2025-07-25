using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IFornecedorService
	{
		Task<IEnumerable<FornecedorDto>> GetAllFornecedorAsync();
		Task<Result<FornecedorDto>> GetFornecedorByIdAsync(Guid id);
		Task<Result<Guid>> CreateFornecedorAsync(CreateFornecedorDto dto);
		Task<Result> UpdateFornecedorAsync(UpdateFornecedorDto dto);
		Task<Result> DeleteFornecedorAsync(Guid id);
		Task<Result> DeleteSoftFornecedorAsync(Guid id);
	}
}
