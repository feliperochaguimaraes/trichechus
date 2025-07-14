using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

// using Trichechus.Application.UseCases.Atividades.CreateAtividade;
// using Trichechus.Application.UseCases.Atividades.GetAtividades;
// using Trichechus.Application.UseCases.atividades.UpdateAtividade;
// using Trichechus.Application.UseCases.Atividades.DeleteAtividade;
// using Trichechus.Application.UseCases.Atividades.UpdateAtividade;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Atividades")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class AtividadesController : ControllerBase
{
	private readonly IAtividadeService _atividadeService;

	// private readonly CreateAtividadeHandler _createHandler;
	// private readonly GetAtividadesHandler _getHandler;
	// private readonly UpdateAtividadeHandler _updateHandler;

	public AtividadesController(IAtividadeService atividadeService)
	{
		_atividadeService = atividadeService;
	}
	/// <summary>
	/// Obtém todas as atividades
	/// </summary>
	/// <returns>Lista de atividades</returns>
	[HttpGet]
	// [SwaggerOperation(Summary = "Obtém todas as atividades", Description = "Retorna uma lista com todas as atividades cadastradas")]
	// [SwaggerResponse(200, "Lista de atividades retornada com sucesso", typeof(IEnumerable<AtividadeDTO>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [AllowAnonymous]
	[Authorize(Roles = "T_LIS_ATI")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			Console.WriteLine("➡️  Iniciando GetAll");

			var atividades = await _atividadeService.GetAllAtividadesAsync();

			Console.WriteLine("✅ Sucesso no GetAll");
			return Ok(atividades);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return StatusCode(500, "Erro interno no servidor: " + ex.Message);
		}
		
		// var atividades = await _atividadeService.GetAllAtividadesAsync();
		// return Ok(atividades);
	}
	
	/// <summary>
	/// Obtém uma atividade pelo ID
	/// </summary>
	/// <param name="id">ID da atividade</param>
	/// <returns>Dados da atividade</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém uma atividade pelo ID", Description = "Retorna os dados de uma atividade específica")]
	[SwaggerResponse(200, "Atividade encontrada com sucesso", typeof(AtividadeDto))]
	[SwaggerResponse(404, "Atividade não encontrada")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _atividadeService.GetAtividadeByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria uma nova atividade
	/// </summary>
	/// <param name="dto">Dados da atividade</param>
	/// <returns>ID da atividade criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria uma nova atividade", Description = "Cria uma nova atividade com os dados fornecidos")]
	[SwaggerResponse(201, "Atividade criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_ATI")]
	public async Task<IActionResult> Create([FromBody] CreateAtividadeDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _atividadeService.CreateAtividadeAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza uma atividade existente
	/// </summary>
	/// <param name="id">ID da atividade</param>
	/// <param name="dto">Novos dados da atividade</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza uma atividade existente", Description = "Atualiza os dados de uma atividade existente")]
	[SwaggerResponse(204, "Atividade atualizada com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Atividade não encontrada")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAtividadeDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _atividadeService.UpdateAtividadeAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove uma atividade
	/// </summary>
	/// <param name="id">ID da atividade</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove uma atividade", Description = "Remove uma atividade existente")]
	[SwaggerResponse(204, "Atividade removida com sucesso")]
	[SwaggerResponse(404, "Atividade não encontrada")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _atividadeService.DeleteSoftAtividadeAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}
}