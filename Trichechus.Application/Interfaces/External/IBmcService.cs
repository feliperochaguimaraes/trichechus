using Trichechus.Application.Common;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Interfaces.External;
public interface IBmcService
{
	Task<string> LoginAsync();
	// Task<string> CriarIncidenteAsync(IncidenteDto dto);
	// Task<string> CriarWorkOrderAsync(WorkOrderDto dto);
}
