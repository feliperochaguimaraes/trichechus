using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trichechus.Application.DTOs;
using Trichechus.Application.Services;

namespace Trichechus.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Tags("Segurança")] // Adiciona uma tag para agrupar endpoints
						// [Authorize(Roles = "GerenciarPerfis")] // *** Protegido por Role/Funcionalidade de Admin ***
	[Authorize]
	public class PerfisController : ControllerBase
	{
		private readonly IPerfilService _perfilService;

		public PerfisController(IPerfilService perfilService)
		{
			_perfilService = perfilService;
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Lista todos os perfis")]
		[SwaggerResponse(200, "Lista de perfis", typeof(IEnumerable<PerfilDto>))]
		[SwaggerResponse(403, "Não Autorizado")]
		[Authorize(Roles = "T_LIS_PER")]
		public async Task<IActionResult> GetAll()
		{
			foreach (var claim in User.Claims)
			{
				Console.WriteLine($"CLAIM: {claim.Type} = {claim.Value}");
			}
			var result = await _perfilService.GetAllAsync();
			return Ok(result.Value);
		}

		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Obtém um perfil pelo ID, incluindo funcionalidades")]
		[SwaggerResponse(200, "Perfil encontrado", typeof(PerfilDto))]
		[SwaggerResponse(403, "Não Autorizado")]
		[SwaggerResponse(404, "Perfil não encontrado")]
		[Authorize(Roles = "T_LIS_PER")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await _perfilService.GetByIdAsync(id);
			if (!result.IsSuccess) return NotFound(result.Errors);
			return Ok(result.Value);
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Cria um novo perfil")]
		[SwaggerResponse(201, "Perfil criado", typeof(PerfilDto))]
		[SwaggerResponse(400, "Dados inválidos ou nome já existe")]
		[SwaggerResponse(403, "Não Autorizado")]
		[Authorize(Roles = "T_CAD_PER")]
		public async Task<IActionResult> Create([FromBody] CreatePerfilDto dto)
		{
			var result = await _perfilService.CreateAsync(dto);
			if (!result.IsSuccess) return BadRequest(result.Errors);
			return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
		}

		[HttpPut("{id}")]
		[SwaggerOperation(Summary = "Atualiza um perfil existente")]
		[SwaggerResponse(204, "Perfil atualizado com sucesso")]
		[SwaggerResponse(400, "Dados inválidos ou nome já existe em outro perfil")]
		[SwaggerResponse(403, "Não Autorizado")]
		[Authorize(Roles = "T_ALT_PER")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePerfilDto dto)
		{
			var result = await _perfilService.UpdateAsync(id, dto);
			if (!result.IsSuccess)
			{
				if (result.Errors.Any(e => e.Contains("não encontrado")))
					return NotFound(result.Errors);
				return BadRequest(result.Errors);
			}
			return NoContent();
		}

		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Exclui um perfil")]
		[SwaggerResponse(204, "Perfil excluído com sucesso")]
		[SwaggerResponse(404, "Perfil não encontrado")]
		[Authorize(Roles = "T_DEL_PER")]
		// Adicionar validação de dependência no serviço antes de excluir
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _perfilService.DeleteAsync(id);
			if (!result.IsSuccess) return NotFound(result.Errors);
			return NoContent();
		}

		// Endpoints para gerenciar funcionalidades de um perfil

		[HttpPost("{perfilId}/funcionalidades")]
		[SwaggerOperation(Summary = "Adiciona uma funcionalidade a um perfil")]
		[SwaggerResponse(204, "Funcionalidade adicionada com sucesso")]
		[SwaggerResponse(400, "IDs inválidos ou associação já existe")]
		[SwaggerResponse(404, "Perfil ou funcionalidade não encontrada")]
		[Authorize(Roles = "T_CAD_FUN")]
		public async Task<IActionResult> AddFuncionalidade(Guid perfilId, [FromBody] AssociarFuncionalidadePerfilDto dto)
		{
			var result = await _perfilService.AddFuncionalidadeAsync(perfilId, dto.FuncionalidadeId);
			if (!result.IsSuccess)
			{
				if (result.Errors.Any(e => e.Contains("não encontrado")))
					return NotFound(result.Errors);
				return BadRequest(result.Errors);
			}
			return NoContent();
		}

		[HttpDelete("{perfilId}/funcionalidades/{funcionalidadeId}")]
		[SwaggerOperation(Summary = "Remove uma funcionalidade de um perfil")]
		[SwaggerResponse(204, "Funcionalidade removida com sucesso")]
		[SwaggerResponse(404, "Perfil ou funcionalidade não encontrada, ou associação não existe")]
		[Authorize(Roles = "T_DEL_FUN")]
		public async Task<IActionResult> RemoveFuncionalidade(Guid perfilId, Guid funcionalidadeId)
		{
			// O serviço já lida com 'não encontrado', podemos retornar NoContent diretamente ou verificar o resultado
			await _perfilService.RemoveFuncionalidadeAsync(perfilId, funcionalidadeId);
			return NoContent();
		}
	}
}
