using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface ITarefaRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=========================================================
	Task<Tarefa> GetByIdAsync(Guid id);
	Task<IEnumerable<Tarefa>> GetAllAsync();
	Task AddAsync(Tarefa tarefa);
	Task UpdateAsync(Tarefa tarefa);
	Task DeleteAsync(Guid id);
}
