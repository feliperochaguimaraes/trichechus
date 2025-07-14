using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Tarefas")] // Adiciona uma tag para agrupar endpoints
public class TarefasController : ControllerBase
{
	private readonly ITarefaService _tarefaService;

	public TarefasController(ITarefaService tarefaService)
	{
		_tarefaService = tarefaService;
	}

	/// <summary>
	/// Obtém todas as tarefas
	/// </summary>
	/// <returns>Lista de tarefas</returns>
	[HttpGet]
	[SwaggerOperation(Summary = "Obtém todas as tarefas", Description = "Retorna uma lista com todas as tarefas cadastradas")]
	[SwaggerResponse(200, "Lista de tarefas retornada com sucesso", typeof(IEnumerable<TarefaDto>))]
	[AllowAnonymous]
	// [Authorize(Roles = "Admin,Gestor")]
	public async Task<IActionResult> GetAll()
	{
		var tarefas = await _tarefaService.GetAllTarefasAsync();
		return Ok(tarefas);
	}

	/// <summary>
	/// Obtém uma tarefa pelo ID
	/// </summary>
	/// <param name="id">ID da tarefa</param>
	/// <returns>Dados da tarefa</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém uma tarefa pelo ID", Description = "Retorna os dados de uma tarefa específica")]
	[SwaggerResponse(200, "Tarefa encontrada com sucesso", typeof(TarefaDto))]
	[SwaggerResponse(404, "Tarefa não encontrada")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _tarefaService.GetTarefaByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}
	/// <summary>
	/// Obtém todas as tarefas pelo ID da atividade
	/// </summary>
	/// <param atividadeId="atividadeId">ID da atividade</param>
	/// <returns>Dados das tarefas</returns>
	[HttpGet("atividade/{atividadeId}")]
	[SwaggerOperation(Summary = "Obtém todas as tarefas pelo ID da atividade", Description = "Retorna os dados de todas as tarefa de uma atividade específica")]
	[SwaggerResponse(200, "Tarefas encontradas com sucesso", typeof(TarefaDto))]
	[SwaggerResponse(404, "Tarefas não encontradas")]
	public async Task<IActionResult> GetByAtividadeId(Guid atividadeId)
	{
		var tarefas = await _tarefaService.GetTarefasByAtividadeIdAsync(atividadeId);
		return Ok(tarefas);
	}

	/// <summary>
	/// Cria uma nova tarefa
	/// </summary>
	/// <param name="dto">Dados da tarefa</param>
	/// <returns>ID da tarefa criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria uma nova tarefa", Description = "Cria uma nova tarefa com os dados fornecidos")]
	[SwaggerResponse(201, "Tarefa criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateTarefaDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _tarefaService.CreateTarefaAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza uma tarefa existente
	/// </summary>
	/// <param name="id">ID da tarefa</param>
	/// <param name="dto">Novos dados da tarefa</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza uma tarefa existente", Description = "Atualiza os dados de uma tarefa existente")]
	[SwaggerResponse(204, "Tarefa atualizada com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(404, "Tarefa não encontrada")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTarefaDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _tarefaService.UpdateTarefaAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove uma tarefa
	/// </summary>
	/// <param name="id">ID da tarefa</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove uma tarefa", Description = "Remove uma tarefa existente")]
	[SwaggerResponse(204, "Tarefa removida com sucesso")]
	[SwaggerResponse(404, "Tarefa não encontrada")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _tarefaService.DeleteTarefaAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}
}