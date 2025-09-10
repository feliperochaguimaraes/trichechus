using Microsoft.AspNetCore.Authorization;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Catalogo")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class CatalogoController : ControllerBase
{
	private readonly ICatalogoService _catalogoService;

	public CatalogoController(ICatalogoService catalogoService)
	{
		_catalogoService = catalogoService;
	}

	/// <summary>
	/// Obtém todos os catalogos
	/// </summary>
	/// <returns>Lista de Cataloges</returns>
	// [HttpGet]
	// [SwaggerOperation(Summary = "Obtém todos os catalogos", Description = "Retorna uma lista com todos os catalogos cadastrados")]
	// [SwaggerResponse(200, "Lista de catalogo retornada com sucesso", typeof(IEnumerable<CatalogoDto>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [SwaggerResponse(404, "Catalogo não encontrado")]
	// [Authorize(Roles = "T_LIS_CAT")]
	// public async Task<IActionResult> GetAll()
	// {
	// 	try
	// 	{
	// 		Console.WriteLine("➡️  Iniciando GetAll");

	// 		var catalogo = await _catalogoService.GetAllCatalogoAsync();

	// 		Console.WriteLine("✅ Sucesso no GetAll");
	// 		return Ok(catalogo);
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
	// 		Console.WriteLine(ex.StackTrace);
	// 		return StatusCode(500, "Erro interno no servidor: " + ex.Message);
	// 	}
	// }
	[HttpGet]
    [SwaggerOperation(Summary = "Obtém todos os catalogos", Description = "Retorna uma lista com todos os catalogos cadastrados")]
    [SwaggerResponse(200, "Lista de catalogos retornada com sucesso", typeof(IEnumerable<CatalogoDto>))]
    [SwaggerResponse(401, "Não autorizado")]
    [AllowAnonymous]
    [Authorize(Roles = "T_LIS_CAT")]
    public async Task<IActionResult> GetCatalogoAll()
    {
        try
        {
            var result = await _catalogoService.GetAllCatalogoAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            var lista = result.Value ?? Enumerable.Empty<CatalogoDto>();
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
	/// Obtém uma catalogo pelo ID
	/// </summary>
	/// <param name="id">ID do catalogo</param>
	/// <returns>Dados do catalogo</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém um catalogo pelo ID", Description = "Retorna os dados de um catalogo específico")]
	[SwaggerResponse(200, "Catalogo encontrado com sucesso", typeof(CatalogoDto))]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Catalogo não encontrado")]
	[Authorize(Roles = "T_LIS_CAT")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _catalogoService.GetCatalogoByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria um novo item no Catálogo
	/// </summary>
	/// <param name="dto">Dados do catalogo</param>
	/// <returns>ID do catalogo criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria um novo item no catálogo", Description = "Cria um novo catalogo com os dados fornecidos")]
	[SwaggerResponse(201, "Catalogo criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_CAT")]
	public async Task<IActionResult> Create([FromBody] CreateCatalogoDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _catalogoService.CreateCatalogoAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);
		// Retorna a funcionalidade criada com o ID
		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza um catalogo existente
	/// </summary>
	/// <param name="id">ID do catalogo</param>
	/// <param name="dto">Novos dados do catalogo</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza um catalogo existente", Description = "Atualiza os dados de um catalogo existente")]
	[SwaggerResponse(204, "Catalogo atualizado com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Catalogo não encontrado")]
	[Authorize(Roles = "T_ALT_CAT")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCatalogoDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _catalogoService.UpdateCatalogoAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove um catalogo
	/// </summary>
	/// <param name="id">ID do catalogo</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove um catalogo", Description = "Remove um catalogo existente")]
	[SwaggerResponse(204, "Catalogo removida com sucesso")]
	[SwaggerResponse(404, "Catalogo não encontrada")]
	[Authorize(Roles = "T_DEL_CAT")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _catalogoService.DeleteCatalogoAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}

}