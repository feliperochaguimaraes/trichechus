using Microsoft.AspNetCore.Authorization; // Para [AllowAnonymous]
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Trichechus.Application.DTOs;
using Trichechus.Application.Services;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Autenticação")] // Adiciona uma tag para agrupar endpoints
[AllowAnonymous] // Permite acesso a este controller sem autenticação prévia
public class LocalAutenticacaoController : ControllerBase
{
	private readonly ILocalAutenticacaoService _localAutenticacaoService;

	public LocalAutenticacaoController(ILocalAutenticacaoService localAutenticacaoService)
	{
		_localAutenticacaoService = localAutenticacaoService;
	}

	/// <summary>
	/// Registra um novo usuário localmente no Trichechus
	/// </summary>
	[HttpPost("registrar")]
	[SwaggerOperation(Summary = "Registra um novo usuário local", Description = "Cria uma nova conta de usuário gerenciada pelo Trichechus")]
	[SwaggerResponse(200, "Usuário registrado com sucesso", typeof(UsuarioTokenDTO))]
	[SwaggerResponse(400, "Dados inválidos ou email já em uso")]
	public async Task<IActionResult> RegistrarLocal([FromBody] RegistroUsuarioLocalDTO dto)
	{
		var result = await _localAutenticacaoService.RegistrarLocalAsync(dto);

		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		// Retorna Ok com o token, pois não há um endpoint GetById para usuários locais ainda
		return Ok(result.Value);
	}

	/// <summary>
	/// Realiza login de usuário local do Trichechus
	/// </summary>
	[HttpPost("login")]
	[SwaggerOperation(Summary = "Realiza login de usuário local", Description = "Autentica um usuário gerenciado pelo Trichechus")]
	[SwaggerResponse(200, "Login realizado com sucesso", typeof(UsuarioTokenDTO))]
	[SwaggerResponse(400, "Credenciais inválidas ou usuário inativo")]
	public async Task<IActionResult> LoginLocal([FromBody] LoginUsuarioDTO dto)
	{
		var result = await _localAutenticacaoService.LoginLocalAsync(dto);

		if (!result.IsSuccess)
			return BadRequest(result.Errors); // Retorna 400 para falha de login

		return Ok(result.Value);
	}
}

