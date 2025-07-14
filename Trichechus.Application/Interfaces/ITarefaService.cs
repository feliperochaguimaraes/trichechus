using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces
{
	public interface ITarefaService
	{
		Task<IEnumerable<TarefaDto>> GetAllTarefasAsync();
		Task<Result<TarefaDto>> GetTarefaByIdAsync(Guid id);
		Task<IEnumerable<TarefaDto>> GetTarefasByAtividadeIdAsync(Guid atividadeId);
		Task<Result<Guid>> CreateTarefaAsync(CreateTarefaDto dto);
		Task<Result> UpdateTarefaAsync(UpdateTarefaDto dto);
		Task<Result> DeleteTarefaAsync(Guid id);
	}
}
