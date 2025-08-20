using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Fornecedor")] // Adiciona uma tag para agrupar endpoints
[Authorize]
public class FornecedorController : ControllerBase
{
	private readonly IFornecedorService _fornecedorService;

	public FornecedorController(IFornecedorService fornecedorService)
	{
		_fornecedorService = fornecedorService;
	}
	/// <summary>
	/// Obtém todos os fornecedores
	/// </summary>
	/// <returns>Lista de Fornecedores</returns>
	[HttpGet]
	// [SwaggerOperation(Summary = "Obtém todos os fornecedores", Description = "Retorna uma lista com todos os fornecedores cadastrados")]
	// [SwaggerResponse(200, "Lista de fornecedor retornada com sucesso", typeof(IEnumerable<FornecedorDTO>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [AllowAnonymous]
	[Authorize(Roles = "T_LIS_FOR")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			Console.WriteLine("➡️  Iniciando GetAll");

			var fornecedor = await _fornecedorService.GetAllFornecedorAsync();

			Console.WriteLine("✅ Sucesso no GetAll");
			return Ok(fornecedor);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
			Console.WriteLine(ex.StackTrace);
			return StatusCode(500, "Erro interno no servidor: " + ex.Message);
		}
	}

	/// <summary>
	/// Obtém uma fornecedor pelo ID
	/// </summary>
	/// <param name="id">ID do fornecedor</param>
	/// <returns>Dados do fornecedor</returns>
	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtém um fornecedor pelo ID", Description = "Retorna os dados de um fornecedor específico")]
	[SwaggerResponse(200, "Fornecedor encontrado com sucesso", typeof(FornecedorDto))]
	[SwaggerResponse(404, "Fornecedor não encontrado")]
	[Authorize(Roles = "T_LIS_FOR")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _fornecedorService.GetFornecedorByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
	}

	/// <summary>
	/// Cria uma nova fornecedor
	/// </summary>
	/// <param name="dto">Dados do fornecedor</param>
	/// <returns>ID do fornecedor criada</returns>
	[HttpPost]
	[SwaggerOperation(Summary = "Cria um novo fornecedor", Description = "Cria uma novo fornecedor com os dados fornecidos")]
	[SwaggerResponse(201, "Fornecedor criada com sucesso", typeof(Guid))]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[Authorize(Roles = "T_CAD_FOR")]
	public async Task<IActionResult> Create([FromBody] CreateFornecedorDto dto)
	{
		// A validação será feita automaticamente pelo FluentValidation
		var result = await _fornecedorService.CreateFornecedorAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
	}

	/// <summary>
	/// Atualiza um fornecedor existente
	/// </summary>
	/// <param name="id">ID do fornecedor</param>
	/// <param name="dto">Novos dados do fornecedor</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpPut("{id}")]
	[SwaggerOperation(Summary = "Atualiza um fornecedor existente", Description = "Atualiza os dados de um fornecedor existente")]
	[SwaggerResponse(204, "Fornecedor atualizado com sucesso")]
	[SwaggerResponse(400, "Dados inválidos")]
	[SwaggerResponse(401, "Não autorizado")]
	[SwaggerResponse(404, "Fornecedor não encontrado")]
	[Authorize(Roles = "T_ALT_FOR")]
	public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFornecedorDto dto)
	{
		if (id != dto.Id)
			return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

		// A validação será feita automaticamente pelo FluentValidation
		var result = await _fornecedorService.UpdateFornecedorAsync(dto);
		if (!result.IsSuccess)
			return BadRequest(result.Errors);

		return NoContent();
	}

	/// <summary>
	/// Remove um fornecedor
	/// </summary>
	/// <param name="id">ID do fornecedor</param>
	/// <returns>Nenhum conteúdo</returns>
	[HttpDelete("{id}")]
	[SwaggerOperation(Summary = "Remove um fornecedor", Description = "Remove um fornecedor existente")]
	[SwaggerResponse(204, "Fornecedor removida com sucesso")]
	[SwaggerResponse(404, "Fornecedor não encontrada")]
	[Authorize(Roles = "T_DEL_FOR")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _fornecedorService.DeleteSoftFornecedorAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}

	// Endpoints para gerenciar contratos de um fornecedor

	[HttpPost("{fornecedorId}/contratos")]
	[SwaggerOperation(Summary = "Adiciona um contrato a um fornecedor")]
	[SwaggerResponse(204, "Contrato adicionado com sucesso")]
	[SwaggerResponse(400, "IDs inválidos ou associação já existe")]
	[SwaggerResponse(404, "Fornecedor ou Contrato não encontrado")]
	[Authorize(Roles = "T_CAD_FOR")]
	public async Task<IActionResult> AddContrato(Guid fornecedorId, [FromBody] AssociarContratoFornecedorDto dto)
	{
		var result = await _fornecedorService.AddContratoAsync(fornecedorId, dto.ContratoId);
		if (!result.IsSuccess)
		{
			if (result.Errors.Any(e => e.Contains("não encontrado")))
				return NotFound(result.Errors);
			return BadRequest(result.Errors);
		}
		return NoContent();
	}

	[HttpDelete("{fornecedorId}/contratos/{contratoId}")]
	[SwaggerOperation(Summary = "Remove um contrato de um fornecedor")]
	[SwaggerResponse(204, "contrato removido com sucesso")]
	[SwaggerResponse(404, "Fornecedor ou Contrato não encontrado, ou associação não existe")]
	[Authorize(Roles = "T_DEL_FOR")]
	public async Task<IActionResult> RemoveContrato(Guid fornecedorId, Guid contratoId)
	{
		// O serviço já lida com 'não encontrado', podemos retornar NoContent diretamente ou verificar o resultado
		await _fornecedorService.RemoveContratoAsync(fornecedorId, contratoId);
		return NoContent();
	}


}