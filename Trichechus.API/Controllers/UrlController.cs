
using Microsoft.AspNetCore.Authorization;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Services;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Url")] // Adiciona uma tag para agrupar endpoints
[Authorize]


public class UrlController : ControllerBase
{
	private readonly IURLService _urlService;

	public UrlController(IURLService urlService)
	{
		_urlService = urlService;
	}

	/// <summary>
	/// Obtém todas as Url
	/// </summary>
	/// <returns>Lista de Url</returns>
	[HttpGet]
	[SwaggerOperation(Summary = "Obtém Obtém todas as url", Description = "Retorna uma lista com todas as url cadastradas")]
	[SwaggerResponse(200, "Lista de url retornada com sucesso", typeof(IEnumerable<UrlDto>))]
	[SwaggerResponse(401, "Não autorizado")]
	[AllowAnonymous]
	[Authorize(Roles = "T_LIS_URL")]
	public async Task<IActionResult> GetUrlAll()
	{
		try
		{
			var result = await _urlService.GetAllUrlAsync();

			if (!result.IsSuccess)
				return BadRequest(result.Errors);
			var lista = result.Value ?? Enumerable.Empty<UrlDto>();
			return Ok(lista);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return Problem(title: "Erro interno no servidor", detail: ex.Message, statusCode: 500);
		}
	}

	/// <summary>
	/// Obtém uma Url pelo ID
	/// </summary>
	/// <param name="id">ID da Url</param>
	/// <returns>Dados da Url</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém uma Url pelo ID", Description = "Retorna os dados de uma Url específica")]
	[SwaggerResponse(200, "Url encontrado com sucesso", typeof(CatalogoDto))]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Url não encontrada")]
	[Authorize(Roles = "T_LIS_URL")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _urlService.GetUrlByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria um novo item na Url
	/// </summary>
	/// <param name="dto">Dados da url</param>
	/// <returns>ID da url criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria um novo item na url", Description = "Cria uma nova url com os dados fornecidos")]
	[SwaggerResponse(201, "Url criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_URL")]
	public async Task<IActionResult> Create([FromBody] CreateUrlDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _urlService.CreateUrlAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);
		// Retorna a funcionalidade criada com o ID
		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza uma url existente
	/// </summary>
	/// <param name="id">ID da url</param>
	/// <param name="dto">Novos dados da url</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza uma url existente", Description = "Atualiza os dados de uma url existente")]
	[SwaggerResponse(204, "Url atualizado com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Url não encontrado")]
	[Authorize(Roles = "T_ALT_URL")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUrlDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _urlService.UpdateUrlAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove uma url
	/// </summary>
	/// <param name="id">ID da url</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove uma url", Description = "Remove uma url existente")]
	[SwaggerResponse(204, "Url removida com sucesso")]
	[SwaggerResponse(404, "Url não encontrada")]
	[Authorize(Roles = "T_DEL_URL")]
	public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _urlService.DeleteUrlAsync(id);
        if (!result.IsSuccess) return NotFound(result.Errors);
        return NoContent();
    }


	// Endpoints para gerenciar software de uma url

	[HttpPost("{urlId}/software")]
	[SwaggerOperation(Summary = "Adiciona um software a uma url")]
	[SwaggerResponse(204, "Software adicionado com sucesso")]
	[SwaggerResponse(400, "IDs inválidos ou associação já existe")]
	[SwaggerResponse(404, "Url ou software não encontrado")]
	[Authorize(Roles = "T_CAD_URL")]
	public async Task<IActionResult> AddSoftware(Guid urlId, [FromBody] AssociarSoftwareUrlDto dto)
	{
		var result = await _urlService.AddSoftUrlAsync(urlId, dto.SoftwareId);
		if (!result.IsSuccess)
		{
			if (result.Errors.Any(e => e.Contains("não encontrado")))
				return NotFound(result.Errors);
			return BadRequest(result.Errors);
		}
		return NoContent();
	}


	[HttpDelete("{urlId}/software/{SoftwareId}")]
	[SwaggerOperation(Summary = "Remove uma url de um software")]
	[SwaggerResponse(204, "Url removido com sucesso")]
	[SwaggerResponse(404, "Url ou software não encontrado, ou associação não existe")]
	[Authorize(Roles = "T_DEL_URL")]
	public async Task<IActionResult> RemoveSoftware(Guid urlId, Guid softwareId)
	{
		// O serviço já lida com 'não encontrado', podemos retornar NoContent diretamente ou verificar o resultado
		await _urlService.DeleteSoftUrlAsync(urlId, softwareId);
		return NoContent();
	}
}