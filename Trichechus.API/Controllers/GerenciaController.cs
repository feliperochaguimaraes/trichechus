using Microsoft.AspNetCore.Authorization;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Gerencia")] // Adiciona uma tag para agrupar endpoints
// [Authorize]
public class GerenciaController : ControllerBase
{
	private readonly IGerenciaService _gerenciaService;

	public GerenciaController(IGerenciaService gerenciaService)
	{
		_gerenciaService = gerenciaService;
	}

	/// <summary>
	/// Obtém todas as gerencias
	/// </summary>
	/// <returns>Lista de gerencias</returns>
	[HttpGet]
	// [SwaggerOperation(Summary = "Obtém todas as gerencias", Description = "Retorna uma lista com todas as gerencias cadastradas")]
	// [SwaggerResponse(200, "Lista de gerencias retornada com sucesso", typeof(IEnumerable<GerenciaDTO>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [AllowAnonymous]
	[Authorize(Roles = "T_LIS_GER")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			Console.WriteLine("➡️  Iniciando GetAll");

			var gerencias = await _gerenciaService.GetAllAsync();

			Console.WriteLine("✅ Sucesso no GetAll");
			return Ok(gerencias);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return StatusCode(500, "Erro interno no servidor: " + ex.Message);
		}
		
		// var gerencias = await _gerenciaService.GetAllGerenciaAsync();
		// return Ok(gerencias);
	}
	
	/// <summary>
	/// Obtém uma gerencia pelo ID
	/// </summary>
	/// <param name="id">ID da gerencia</param>
	/// <returns>Dados da gerencia</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém uma gerencia pelo ID", Description = "Retorna os dados de uma gerencia específica")]
	[SwaggerResponse(200, "Gerencia encontrada com sucesso", typeof(GerenciaDto))]
	[SwaggerResponse(404, "Gerencia não encontrada")]
	[Authorize(Roles = "T_LIS_GER")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _gerenciaService.GetByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria uma nova gerencia
	/// </summary>
	/// <param name="dto">Dados da gerencia</param>
	/// <returns>ID da gerencia criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria uma nova gerencia", Description = "Cria uma nova gerencia com os dados fornecidos")]
	[SwaggerResponse(201, "Gerencia criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_GER")]
	public async Task<IActionResult> Create([FromBody] CreateGerenciaDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _gerenciaService.CreateAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza uma gerencia existente
	/// </summary>
	/// <param name="id">ID da gerencia</param>
	/// <param name="dto">Novos dados da gerencia</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza uma gerencia existente", Description = "Atualiza os dados de uma gerencia existente")]
	[SwaggerResponse(204, "Gerencia atualizada com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Gerencia não encontrada")]
	[Authorize(Roles = "T_ALT_GER")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGerenciaDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _gerenciaService.UpdateAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove uma gerencia
	/// </summary>
	/// <param name="id">ID da gerencia</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove uma gerencia", Description = "Remove uma gerencia existente")]
	[SwaggerResponse(204, "Gerencia removida com sucesso")]
	[SwaggerResponse(404, "Gerencia não encontrada")]
	[Authorize(Roles = "T_DEL_GER")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _gerenciaService.DeleteAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}
}