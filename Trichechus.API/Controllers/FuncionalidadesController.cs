using Microsoft.AspNetCore.Authorization;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Funcionalidade")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class FuncionalidadesController : ControllerBase
{
	private readonly IFuncionalidadeService _funcionalidadeService;

	public FuncionalidadesController(IFuncionalidadeService funcionalidadeService)
	{
		_funcionalidadeService = funcionalidadeService;
	}

	/// <summary>
	/// Obtém todos os funcionalidades
	/// </summary>
	/// <returns>Lista de Funcionalidades</returns>
	[HttpGet]
	[SwaggerOperation(Summary = "Lista todas as funcionalidades")]
	[SwaggerResponse(200, "Lista de funcionalidades", typeof(IEnumerable<FuncionalidadeDto>))]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Funcionalidade não encontrada")]
	[Authorize(Roles = "T_LIS_FUN")]
	public async Task<IActionResult> GetAllFuncionalidade()
	{
		var result = await _funcionalidadeService.GetAllFuncionalidadeAsync();
		return Ok(result.Value);
	}

	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém uma funcionalidade pelo ID")]
	[SwaggerResponse(200, "Funcionalidade encontrada", typeof(FuncionalidadeDto))]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Funcionalidade não encontrada")]
	[Authorize(Roles = "T_LIS_FUN")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _funcionalidadeService.GetFuncionalidadeByIdAsync(id);
		if (!result.IsSuccess) return NotFound(result.Errors);
		return Ok(result.Value);
	}

	[HttpPost]
	[SwaggerOperation(Summary = "Cria uma nova funcionalidade")]
	[SwaggerResponse(201, "Funcionalidade criada", typeof(FuncionalidadeDto))]
	[SwaggerResponse(400, "Dados inválidos ou nome já existe")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_FUN")]
	public async Task<IActionResult> Create([FromBody] CreateFuncionalidadeDto dto)
	{
		var result = await _funcionalidadeService.CreateFuncionalidadeAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);
		// Retorna a funcionalidade criada com o ID
		return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
	}

	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza uma funcionalidade existente")]
	[SwaggerResponse(204, "Funcionalidade atualizada com sucesso")]
	[SwaggerResponse(400, "Dados inválidos ou nome já existe em outra funcionalidade")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Funcionalidade não encontrada")]
	[Authorize(Roles = "T_ALT_FUN")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFuncionalidadeDto dto)
	{
		var result = await _funcionalidadeService.UpdateFuncionalidadeAsync(id, dto);
		if (!result.IsSuccess)
		{
			// Verifica se o erro é 'não encontrado' para retornar 404
			if (result.Errors.Any(e => e.Contains("não encontrada")))
				return NotFound(result.Errors);
			return BadRequest(result.Errors);
		}
		return NoContent();
	}

	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Exclui uma funcionalidade")]
	[SwaggerResponse(204, "Funcionalidade excluída com sucesso")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Funcionalidade não encontrada")]
	[Authorize(Roles = "T_DEL_FUN")]
	// Adicionar validação de dependência no serviço antes de excluir
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _funcionalidadeService.DeleteFuncionalidadeAsync(id);
		if (!result.IsSuccess) return NotFound(result.Errors);
		return NoContent();
	}
}