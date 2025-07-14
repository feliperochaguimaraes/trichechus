using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Software")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class SoftwareController : ControllerBase
{
	private readonly ISoftwareService _softwareService;

	public SoftwareController(ISoftwareService softwareService)
	{
		_softwareService = softwareService;
	}
	/// <summary>
	/// Obtém todos os softwarees
	/// </summary>
	/// <returns>Lista de Softwarees</returns>
	[HttpGet]
	// [SwaggerOperation(Summary = "Obtém todos os softwarees", Description = "Retorna uma lista com todos os softwarees cadastrados")]
	// [SwaggerResponse(200, "Lista de software retornada com sucesso", typeof(IEnumerable<SoftwareDTO>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [AllowAnonymous]
	[Authorize(Roles = "T_LIS_SOF")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			Console.WriteLine("➡️  Iniciando GetAll");

			var software = await _softwareService.GetAllSoftwareAsync();

			Console.WriteLine("✅ Sucesso no GetAll");
			return Ok(software);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return StatusCode(500, "Erro interno no servidor: " + ex.Message);
		}
	}

	/// <summary>
	/// Obtém uma software pelo ID
	/// </summary>
	/// <param name="id">ID do software</param>
	/// <returns>Dados do software</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém um software pelo ID", Description = "Retorna os dados de um software específico")]
	[SwaggerResponse(200, "Software encontrado com sucesso", typeof(SoftwareDto))]
	[SwaggerResponse(404, "Software não encontrado")]
	[Authorize(Roles = "T_LIS_SOF")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _softwareService.GetSoftwareByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria uma nova software
	/// </summary>
	/// <param name="dto">Dados do software</param>
	/// <returns>ID do software criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria um novo software", Description = "Cria uma novo software com os dados fornecidos")]
	[SwaggerResponse(201, "Software criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_SOF")]
	public async Task<IActionResult> Create([FromBody] CreateSoftwareDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _softwareService.CreateSoftwareAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza um software existente
	/// </summary>
	/// <param name="id">ID do software</param>
	/// <param name="dto">Novos dados do software</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza um software existente", Description = "Atualiza os dados de um software existente")]
	[SwaggerResponse(204, "Software atualizado com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Software não encontrado")]
	[Authorize(Roles = "T_ALT_SOF")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSoftwareDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _softwareService.UpdateSoftwareAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove um software
	/// </summary>
	/// <param name="id">ID do software</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove um software", Description = "Remove um software existente")]
	[SwaggerResponse(204, "Software removida com sucesso")]
	[SwaggerResponse(404, "Software não encontrada")]
	[Authorize(Roles = "T_DEL_SOF")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _softwareService.DeleteSoftwareAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}
}