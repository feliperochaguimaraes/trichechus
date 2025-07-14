using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface IAtividadeService
	{
		Task<IEnumerable<AtividadeDto>> GetAllAtividadesAsync();
		Task<Result<AtividadeDto>> GetAtividadeByIdAsync(Guid id);
		Task<Result<Guid>> CreateAtividadeAsync(CreateAtividadeDto dto);
		Task<Result> UpdateAtividadeAsync(UpdateAtividadeDto dto);
		Task<Result> DeleteAtividadeAsync(Guid id);
		Task<Result> DeleteSoftAtividadeAsync(Guid id);
	}
}
