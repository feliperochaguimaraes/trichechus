using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IAtividadeRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=============================================
	Task<Atividade> GetByIdAsync(Guid id);
	Task<IEnumerable<Atividade>> GetAllAsync();
	Task AddAsync(Atividade atividade);
	Task UpdateAsync(Atividade atividade);
	Task DeleteAsync(Guid id);
	Task DeleteSoftAsync(Guid id);
}
