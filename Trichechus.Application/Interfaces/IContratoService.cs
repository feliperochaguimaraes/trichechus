using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IContratoService
	{
		Task<IEnumerable<ContratoDto>> GetAllContratoAsync();
		Task<Result<ContratoDto>> GetContratoByIdAsync(Guid id);
		Task<Result<Guid>> CreateContratoAsync(CreateContratoDto dto);
		Task<Result> UpdateContratoAsync(UpdateContratoDto dto);
		Task<Result> DeleteContratoAsync(Guid id);
	}
}
