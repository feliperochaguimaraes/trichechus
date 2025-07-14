using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Contrato")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class ContratoController : ControllerBase
{
	private readonly IContratoService _contratoService;

	public ContratoController(IContratoService contratoService)
	{
		_contratoService = contratoService;
	}
	/// <summary>
	/// Obtém todos os contratoes
	/// </summary>
	/// <returns>Lista de Contratoes</returns>
	[HttpGet]
	// [SwaggerOperation(Summary = "Obtém todos os contratoes", Description = "Retorna uma lista com todos os contratoes cadastrados")]
	// [SwaggerResponse(200, "Lista de contrato retornada com sucesso", typeof(IEnumerable<ContratoDTO>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [AllowAnonymous]
	[Authorize(Roles = "T_LIS_CON")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			Console.WriteLine("➡️  Iniciando GetAll");

			var contrato = await _contratoService.GetAllContratoAsync();

			Console.WriteLine("✅ Sucesso no GetAll");
			return Ok(contrato);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return StatusCode(500, "Erro interno no servidor: " + ex.Message);
		}
	}

	/// <summary>
	/// Obtém uma contrato pelo ID
	/// </summary>
	/// <param name="id">ID do contrato</param>
	/// <returns>Dados do contrato</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém um contrato pelo ID", Description = "Retorna os dados de um contrato específico")]
	[SwaggerResponse(200, "Contrato encontrado com sucesso", typeof(ContratoDto))]
	[SwaggerResponse(404, "Contrato não encontrado")]
	[Authorize(Roles = "T_LIS_CON")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _contratoService.GetContratoByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria uma nova contrato
	/// </summary>
	/// <param name="dto">Dados do contrato</param>
	/// <returns>ID do contrato criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria um novo contrato", Description = "Cria uma novo contrato com os dados fornecidos")]
	[SwaggerResponse(201, "Contrato criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_CON")]
	public async Task<IActionResult> Create([FromBody] CreateContratoDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _contratoService.CreateContratoAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza um contrato existente
	/// </summary>
	/// <param name="id">ID do contrato</param>
	/// <param name="dto">Novos dados do contrato</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza um contrato existente", Description = "Atualiza os dados de um contrato existente")]
	[SwaggerResponse(204, "Contrato atualizado com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Contrato não encontrado")]
	[Authorize(Roles = "T_ALT_CON")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContratoDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _contratoService.UpdateContratoAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove um contrato
	/// </summary>
	/// <param name="id">ID do contrato</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove um contrato", Description = "Remove um contrato existente")]
	[SwaggerResponse(204, "Contrato removida com sucesso")]
	[SwaggerResponse(404, "Contrato não encontrada")]
	[Authorize(Roles = "T_DEL_CON")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _contratoService.DeleteSoftContratoAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}
}