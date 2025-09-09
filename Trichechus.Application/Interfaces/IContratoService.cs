using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Domain.Entities;

namespace Trichechus.Application.Interfaces
{
	public interface IContratoService
	{
		Task<Result<ContratoDto>> GetByIdAsync(Guid id);
		Task<Result<IEnumerable<ContratoDto>>>GetAllContratoAsync();
		Task<Result<ContratoDto>> GetContratoByIdAsync(Guid id);
		Task<Result<Guid>> CreateContratoAsync(CreateContratoDto dto);
		Task<Result> UpdateContratoAsync(UpdateContratoDto dto);
		Task<Result> DeleteContratoAsync(Guid id);
		Task<Result> AddFornecedorAsync(Guid contratoId, Guid fornecedorId);
		Task <Result> RemoveFornecedorAsync(Guid contratoId, Guid fornecedorId);
	}
}
