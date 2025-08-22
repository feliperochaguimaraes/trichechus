using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IFornecedorService
	{
		Task<Result<IEnumerable<FornecedorDto>>> GetAllAsync();
		Task<Result<FornecedorDto>> GetFornecedorByIdAsync(Guid id);
		Task<Result<Guid>> CreateFornecedorAsync(CreateFornecedorDto dto);
		Task<Result> UpdateFornecedorAsync(UpdateFornecedorDto dto);
		Task<Result> DeleteFornecedorAsync(Guid id);
		Task<Result> AddContratoAsync(Guid fornecedorId, Guid contratoId);
		Task <Result> RemoveContratoAsync(Guid fornecedorId, Guid contratoId);
    }
}
