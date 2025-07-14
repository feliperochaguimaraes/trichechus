// API/Controllers/SGAAutenticacaoController.cs
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Trichechus.Application.DTOs;
using Trichechus.Application.Services;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Autenticação")] // Adiciona uma tag para agrupar endpoints
public class SGAAutenticacaoController : ControllerBase
{
	private readonly ISGAAutenticacaoService _sgaAutenticacaoService;

	public SGAAutenticacaoController(ISGAAutenticacaoService sgaAutenticacaoService)
	{
		_sgaAutenticacaoService = sgaAutenticacaoService;
	}

	/// <summary>
	/// Realiza login de usuário via SGA
	/// </summary>
	[HttpPost("login")]
	[SwaggerOperation(Summary = "Realiza login de usuário via SGA", Description = "Autentica um usuário através do sistema SGA")]
	[SwaggerResponse(200, "Login realizado com sucesso", typeof(UsuarioTokenDTO))]
	// [SwaggerResponse(400, "Credenciais inválidas")]
	public async Task<IActionResult> Login([FromBody] LoginUsuarioDTO dto)
	{
		var result = await _sgaAutenticacaoService.LoginAsync(dto);

		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return Ok(result.Value);
	}
}

