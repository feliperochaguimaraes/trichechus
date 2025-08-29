using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Trichechus.Domain.Entities;

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
	/// Obtém todos os contratos
	/// </summary>
	/// <returns>Lista de Contratoes</returns>
	// [HttpGet]
	// [SwaggerOperation(Summary = "Obtém todos os contratoes", Description = "Retorna uma lista com todos os contratoes cadastrados")]
	// [SwaggerResponse(200, "Lista de contrato retornada com sucesso", typeof(IEnumerable<ContratoDto>))]
	// [SwaggerResponse(401, "Não autorizado")]
	// [AllowAnonymous]
	// [Authorize(Roles = "T_LIS_CON")]
	// public async Task<IActionResult> GetAll()
	// {
	// 	try
	// 	{
	// 		Console.WriteLine("➡️  Iniciando GetAll");

	// 		var contrato = await _contratoService.GetAllContratoAsync();

	// 		Console.WriteLine("✅ Sucesso no GetAll");
	// 		return Ok(contrato);
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		Console.WriteLine("❌ Erro em GetAll: " + ex.Message);
	// 		Console.WriteLine(ex.StackTrace);
	// 		return StatusCode(500, "Erro interno no servidor: " + ex.Message);
	// 	}

	// }
	[HttpGet]
	[SwaggerOperation(Summary = "Obtém todos os contratos", Description = "Retorna uma lista com todos os contratos cadastrados")]
	[SwaggerResponse(200, "Lista de contrato retornada com sucesso", typeof(IEnumerable<ContratoDto>))]
	[SwaggerResponse(401, "Não autorizado")]
	[AllowAnonymous]
	[Authorize(Roles = "T_LIS_CON")]
	public async Task<IActionResult> GetContratoAll()
	{
		try
		{
			var result = await _contratoService.GetAllContratoAsync();

			if (!result.IsSuccess)
				return BadRequest(result.Errors);

			
			var lista = result.Value ?? Enumerable.Empty<ContratoDto>();
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
	/// Obtém uma contrato pelo ID
	/// </summary>
	/// <param name="id">ID do contrato</param>
	/// <returns>Dados do contrato</returns>
	[HttpGet("{id:guid}")]
	[SwaggerOperation(Summary = "Obtém um contrato pelo ID, incluido Fornecedor", Description = "Retorna os dados de um contrato específico")]
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
		var result = await _contratoService.DeleteContratoAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return NoContent();
	}

	// Endpoints para gerenciar fornecedor de um contrato

	[HttpPost("{contratoId}/fornecedores")]
	[SwaggerOperation(Summary = "Adiciona um fornecedor a um contrato")]
	[SwaggerResponse(204, "Fornecedor adicionado com sucesso")]
	[SwaggerResponse(400, "IDs inválidos ou associação já existe")]
	[SwaggerResponse(404, "Contrato ou Fornecedor não encontrado")]
	[Authorize(Roles = "T_CAD_CON")]
	public async Task<IActionResult> AddFornecedor(Guid contratoId, [FromBody] AssociarFornecedorContratoDto dto)
	{
		var result = await _contratoService.AddFornecedorAsync(contratoId, dto.FornecedorId);
		if (!result.IsSuccess)
		{
			if (result.Errors.Any(e => e.Contains("não encontrado")))
				return NotFound(result.Errors);
			return BadRequest(result.Errors);
		}
		return NoContent();
	}

	[HttpDelete("{contratoId}/fornecedores/{fornecedorId}")]
	[SwaggerOperation(Summary = "Remove um fornecedor de um contrato")]
	[SwaggerResponse(204, "contrato removido com sucesso")]
	[SwaggerResponse(404, "Contrato ou Fornecedor não encontrado, ou associação não existe")]
	[Authorize(Roles = "T_DEL_CON")]
	public async Task<IActionResult> RemoveFornecedor(Guid contratoId, Guid fornecedorId)
	{
		// O serviço já lida com 'não encontrado', podemos retornar NoContent diretamente ou verificar o resultado
		await _contratoService.RemoveFornecedorAsync(contratoId, fornecedorId);
		return NoContent();
	}

}