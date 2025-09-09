using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces.External;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Helix")] // Adiciona uma tag para agrupar endpoints
				// [Authorize(Roles = "GerenciarPerfis")] // *** Protegido por Role/Funcionalidade de Admin ***
[Authorize]
public class HelixController : ControllerBase
{
	private readonly IBmcService _bmcService;

	public HelixController(IBmcService bmcService)
	{
		_bmcService = bmcService;
	}

	// [HttpGet("chamados")]
	// public async Task<IActionResult> BuscarChamados([FromQuery] string tipo, [FromQuery] string numero, [FromQuery] string descricao)
	// {
	// 	var resultado = await _bmcService.BuscarChamadosAsync(tipo, numero, descricao);
	// 	return Ok(resultado);
	// }

}