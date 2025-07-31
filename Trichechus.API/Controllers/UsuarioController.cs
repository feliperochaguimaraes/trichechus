using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Services;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Segurança")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class UsuarioController : ControllerBase
{
	private readonly IUsuarioService _usuarioService;

	public UsuarioController(IUsuarioService usuarioService)
	{
		_usuarioService = usuarioService;
	}

	[HttpGet]
	[SwaggerOperation(Summary = "Lista todos os usuarios")]
	[SwaggerResponse(200, "Lista de usuarios", typeof(IEnumerable<UsuarioDto>))]
	[SwaggerResponse(403, "Não Autorizado")]
	[Authorize(Roles = "T_LIS_USU")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			Console.WriteLine("➡️  Iniciando GetAll");

			var usuarios = await _usuarioService.GetAllAsync();

			Console.WriteLine("✅ Sucesso no GetAll");
			return Ok(usuarios);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return StatusCode(500, "Erro interno no servidor: " + ex.Message);
		}
	}

	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém um usuario pelo ID, incluindo funcionalidades")]
	[SwaggerResponse(200, "Usuario encontrado", typeof(UsuarioDto))]
	[SwaggerResponse(403, "Não Autorizado")]
	[SwaggerResponse(404, "Usuario não encontrado")]
	[Authorize(Roles = "T_LIS_USU")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _usuarioService.GetByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);
		return Ok(result.Value);
	}

	[HttpPost]
	[SwaggerOperation(Summary = "Cria um novo Usuário")]
	[SwaggerResponse(201, "Usuário criado", typeof(UsuarioDto))]
	[SwaggerResponse(400, "Dados inválidos ou nome já existe")]
	[SwaggerResponse(403, "Não Autorizado")]
	[Authorize(Roles = "T_CAD_USU")]
	public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
	{
		var result = await _usuarioService.CreateAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);
		return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
	}

	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza um usuário existente")]
	[SwaggerResponse(204, "Usuário atualizado com sucesso")]
	[SwaggerResponse(400, "Dados inválidos ou nome já existe em outro usuario")]
	[SwaggerResponse(403, "Não Autorizado")]
	[Authorize(Roles = "T_ALT_USU")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioDto dto)
	{
		var result = await _usuarioService.UpdateAsync(id, dto);
		if (!result.IsSuccess)
		{
			if (result.Errors.Any(e => e.Contains("não encontrado")))
				return NotFound(result.Errors);
			return BadRequest(result.Errors);
		}
		return NoContent();
	}

	// Endpoints para gerenciar funcionalidades de um usuario

	[HttpPost("{usuarioId}/perfil")]
	[SwaggerOperation(Summary = "Adiciona um perfil a um usuário")]
	[SwaggerResponse(204, "Perfil adicionado com sucesso")]
	[SwaggerResponse(400, "IDs inválidos ou associação já existe")]
	[SwaggerResponse(404, "Usuario ou perfil não encontrado")]
	[Authorize(Roles = "T_CAD_USU")]
	public async Task<IActionResult> AddPerfil(Guid usuarioId, [FromBody] AssociarPerfilUsuarioDto dto)
	{
		var result = await _usuarioService.AddPerfilAsync(usuarioId, dto.PerfilId);
		if (!result.IsSuccess)
		{
			if (result.Errors.Any(e => e.Contains("não encontrado")))
				return NotFound(result.Errors);
			return BadRequest(result.Errors);
		}
		return NoContent();
	}

	[HttpDelete("{usuarioId}/perfil/{perfilId}")]
	[SwaggerOperation(Summary = "Remove um perfil de um usuário")]
	[SwaggerResponse(204, "Perfil removido com sucesso")]
	[SwaggerResponse(404, "Usuário ou perfil não encontrado, ou associação não existe")]
	[Authorize(Roles = "T_DEL_USU")]
	public async Task<IActionResult> RemovePerfil(Guid usuarioId, Guid funcionalidadeId)
	{
		// O serviço já lida com 'não encontrado', podemos retornar NoContent diretamente ou verificar o resultado
		await _usuarioService.RemovePerfilAsync(usuarioId, funcionalidadeId);
		return NoContent();
	}
}