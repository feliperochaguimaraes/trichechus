using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Application.Services;
using Trichechus.Domain.Entities;

namespace Trichechus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("BaseDados")] // Adiciona uma tag para agrupar endpoints
                    // [Authorize(Roles = "GerenciarPerfis")] // *** Protegido por Role/Funcionalidade de Admin ***
[Authorize]
public class BaseDadosController : ControllerBase
{
    private readonly IBaseDadosService _baseDadosService;

    public BaseDadosController(IBaseDadosService baseDadosService)
    {
        _baseDadosService = baseDadosService;
    }
    
    /// <summary>
    /// Obtém todas as bases de dados
    /// </summary>
    /// <returns>Lista da base de dados</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todas as bases de dados", Description = "Retorna uma lista com todas bases de dados cadastradas")]
    [SwaggerResponse(200, "Lista de base de dados retornada com sucesso", typeof(IEnumerable<BaseDadosDto>))]
    [SwaggerResponse(401, "Não autorizado")]
    [AllowAnonymous]
    [Authorize(Roles = "T_LIS_BAS")]
    public async Task<IActionResult> GetBaseDeDadosAll()
    {
        try
        {
            var result = await _baseDadosService.GetAllBaseDadosAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            var lista = result.Value ?? Enumerable.Empty<BaseDadosDto>();
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
	/// Obtém uma base de dados pelo ID
	/// </summary>
	/// <param name="id">ID da base de dados</param>
	/// <returns>Dados do software</returns>
	[HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém uma base de dados pelo ID, incluindo software")]
    [SwaggerResponse(200, "Base de dados encontrado", typeof(BaseDadosDto))]
    [SwaggerResponse(403, "Não Autorizado")]
    [SwaggerResponse(404, "Base de dados não encontrado")]
    [Authorize(Roles = "T_LIS_BAS")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _baseDadosService.GetBaseDadosByIdAsync(id);
		if (!result.IsSuccess)
			return NotFound(result.Errors);

		return Ok(result.Value);
    }

    /// <summary>
    /// Cria uma nova base de dados
    /// </summary>
    /// <param name="dto">Dados da base de dados</param>
    /// <returns>ID do software criado</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo contrato", Description = "Cria uma novo contrato com os dados fornecidos")]
    [SwaggerResponse(201, "Contrato criada com sucesso", typeof(Guid))]
    [SwaggerResponse(400, "Dados inválidos")]
    [SwaggerResponse(401, "Não autorizado")]
    [Authorize(Roles = "T_CAD_BAS")]
    public async Task<IActionResult> Create([FromBody] CreateBaseDadosDto dto)
    {
        // A validação será feita automaticamente pelo FluentValidation
        var result = await _baseDadosService.CreateBaseDadosAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Atualiza uma base de dados ja existente
    /// </summary>
    /// <param name="id">ID da base de dados</param>
    /// <param name="dto">Novos dados da base de dados</param>
    /// <returns>Nenhum conteúdo</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza uma base de dados existente", Description = "Atualiza os dados de uma base de dados existente")]
    [SwaggerResponse(204, "Base de dados atualizada com sucesso")]
    [SwaggerResponse(400, "Dados inválidos")]
    [SwaggerResponse(401, "Não autorizado")]
    [SwaggerResponse(404, "Contrato não encontrado")]
    [Authorize(Roles = "T_ALT_BAS")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBaseDadosDto dto)
    {
        if (id != dto.Id)
            return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");

        // A validação será feita automaticamente pelo FluentValidation
        var result = await _baseDadosService.UpdateBaseDadosAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Exclui uma base de dados")]
    [SwaggerResponse(204, "Base de dados excluída com sucesso")]
    [SwaggerResponse(404, "Base de dados não encontrada")]
    [Authorize(Roles = "T_DEL_BAS")]
    // Adicionar validação de dependência no serviço antes de excluir
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _baseDadosService.DeleteBaseDadosAsync(id);
        if (!result.IsSuccess) return NotFound(result.Errors);
        return NoContent();
    }

    // Endpoints para gerenciar software de uma base de dados

    [HttpPost("{baseDadosId}/software")]
    [SwaggerOperation(Summary = "Adiciona um software a uma base de dados")]
    [SwaggerResponse(204, "Software adicionado com sucesso")]
    [SwaggerResponse(400, "IDs inválidos ou associação já existe")]
    [SwaggerResponse(404, "Base de dados ou software não encontrado")]
    [Authorize(Roles = "T_CAD_SOF")]
    public async Task<IActionResult> AddSoftware(Guid baseDadosId, [FromBody] AssociarSoftwareBaseDadosDto dto)
    {
        var result = await _baseDadosService.AddSoftBaseDadosAsync(baseDadosId, dto.SoftwareId);
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(e => e.Contains("não encontrado")))
                return NotFound(result.Errors);
            return BadRequest(result.Errors);
        }
        return NoContent();
    }

    [HttpDelete("{BaseDadosId}/fornecedores/{SoftwareId}")]
	[SwaggerOperation(Summary = "Remove um fornecedor de um contrato")]
	[SwaggerResponse(204, "contrato removido com sucesso")]
	[SwaggerResponse(404, "Contrato ou Fornecedor não encontrado, ou associação não existe")]
	[Authorize(Roles = "T_DEL_BAS")]
	public async Task<IActionResult> RemoveSoftware(Guid BaseDadosId, Guid SoftwareId)
	{
		// O serviço já lida com 'não encontrado', podemos retornar NoContent diretamente ou verificar o resultado
		await _baseDadosService.RemoveSoftBaseDadosAsync(BaseDadosId, SoftwareId);
		return NoContent();
	}
}