using Trichechus.Domain.Entities;

namespace Trichechus.Domain.Interfaces;

public interface IContratoRepository
{
	//PADRONIZAÇÃO DAS FUNÇÕES
	//=============================================
	Task<Contrato> GetByIdAsync(Guid id);
	Task<IEnumerable<Contrato>> GetAllAsync();
	Task AddAsync(Contrato contrato);
	Task UpdateAsync(Contrato contrato);
	Task DeleteAsync(Guid id);
}