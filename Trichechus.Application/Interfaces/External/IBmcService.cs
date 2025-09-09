using Trichechus.Application.Common;
using Trichechus.Application.DTOs.Bmc;

namespace Trichechus.Application.Interfaces.External;

public interface IBmcService
{
	Task<string> LoginAsync();
	// Task<ChamadoDto> GetChamadoByNumberAsync(string chamadoNumber);
	// Task<List<ChamadoDto>> GetChamadoByDescriptionAsync(string description);

	Task<List<ChamadoResponseDto>> BuscarChamadosAsync(
		BmcTicketType tipo, 
		object parametros
	);
}
public enum BmcTicketType
{
	Incident,
	WorkOrder
}