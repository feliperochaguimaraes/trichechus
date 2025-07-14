using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trichechus.Application.DTOs;
using Trichechus.Application.Services;

namespace Trichechus.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Tags("Segurança")] // Adiciona uma tag para agrupar endpoints
	// [Authorize(Roles = "GerenciarFuncionalidades")] // *** Protegido por Role/Funcionalidade de Admin ***
	public class FuncionalidadesController : ControllerBase
	{
		private readonly IFuncionalidadeService _funcionalidadeService;

		public FuncionalidadesController(IFuncionalidadeService funcionalidadeService)
		{
			_funcionalidadeService = funcionalidadeService;
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Lista todas as funcionalidades")]
		[SwaggerResponse(200, "Lista de funcionalidades", typeof(IEnumerable<FuncionalidadeDto>))]
		public async Task<IActionResult> GetAll()
		{
			var result = await _funcionalidadeService.GetAllAsync();
			return Ok(result.Value);
		}

		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Obtém uma funcionalidade pelo ID")]
		[SwaggerResponse(200, "Funcionalidade encontrada", typeof(FuncionalidadeDto))]
		[SwaggerResponse(404, "Funcionalidade não encontrada")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _funcionalidadeService.GetByIdAsync(id);
			if (!result.IsSuccess) return NotFound(result.Errors);
			return Ok(result.Value);
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Cria uma nova funcionalidade")]
		[SwaggerResponse(201, "Funcionalidade criada", typeof(FuncionalidadeDto))]
		[SwaggerResponse(400, "Dados inválidos ou nome já existe")]
		public async Task<IActionResult> Create([FromBody] CreateFuncionalidadeDto dto)
		{
			var result = await _funcionalidadeService.CreateAsync(dto);
			if (!result.IsSuccess) return BadRequest(result.Errors);
			// Retorna a funcionalidade criada com o ID
			return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
		}

		[HttpPut("{id}")]
		[SwaggerOperation(Summary = "Atualiza uma funcionalidade existente")]
		[SwaggerResponse(204, "Funcionalidade atualizada com sucesso")]
		[SwaggerResponse(400, "Dados inválidos ou nome já existe em outra funcionalidade")]
		[SwaggerResponse(404, "Funcionalidade não encontrada")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFuncionalidadeDto dto)
		{
			var result = await _funcionalidadeService.UpdateAsync(id, dto);
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
		[SwaggerResponse(404, "Funcionalidade não encontrada")]
		// Adicionar validação de dependência no serviço antes de excluir
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _funcionalidadeService.DeleteAsync(id);
			if (!result.IsSuccess) return NotFound(result.Errors);
			return NoContent();
		}
	}
}
