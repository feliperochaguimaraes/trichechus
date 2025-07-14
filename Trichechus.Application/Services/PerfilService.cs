using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services
{
	public class PerfilService : IPerfilService
	{
		private readonly IPerfilRepository _perfilRepository;
		private readonly IFuncionalidadeRepository _funcionalidadeRepository;

		public PerfilService(IPerfilRepository perfilRepository, IFuncionalidadeRepository funcionalidadeRepository)
		{
			_perfilRepository = perfilRepository;
			_funcionalidadeRepository = funcionalidadeRepository;
		}

		public async Task<Result<PerfilDto>> GetByIdAsync(Guid id)
		{
			var perfil = await _perfilRepository.GetByIdWithFuncionalidadesAsync(id);
			if (perfil == null) return Result<PerfilDto>.Failure(new[] { "Perfil não encontrado." });

			var dto = MapToDto(perfil);
			return Result<PerfilDto>.Success(dto);
		}

		public async Task<Result<IEnumerable<PerfilDto>>> GetAllAsync()
		{
			var perfis = await _perfilRepository.GetAllAsync();
			// Mapear para DTO (sem funcionalidades para performance, ou carregar se necessário)
			var dtos = perfis.Select(p => new PerfilDto { Id = p.Id, Nome = p.Nome, Descricao = p.Descricao });
			return Result<IEnumerable<PerfilDto>>.Success(dtos);
		}

		public async Task<Result<PerfilDto>> CreateAsync(CreatePerfilDto dto)
		{
			if (await _perfilRepository.GetByNameAsync(dto.Nome) != null)
			{
				return Result<PerfilDto>.Failure(new[] { $"Já existe um perfil com o nome 	'{dto.Nome}	'." });
			}

			var perfil = new Perfil
			{
				Nome = dto.Nome,
				Descricao = dto.Descricao,
				Funcionalidades = new List<Funcionalidade>()
			};

			// Adicionar funcionalidades se IDs foram fornecidos
			if (dto.FuncionalidadeIds != null)
			{
				foreach (var funcId in dto.FuncionalidadeIds)
				{
					var funcionalidade = await _funcionalidadeRepository.GetByIdAsync(funcId);
					if (funcionalidade != null)
					{
						perfil.Funcionalidades.Add(funcionalidade);
					}
					// Opcional: retornar erro se funcionalidade não existir
				}
			}

			await _perfilRepository.AddAsync(perfil);
			var resultDto = MapToDto(perfil);
			return Result<PerfilDto>.Success(resultDto);
		}

		public async Task<Result> UpdateAsync(Guid id, UpdatePerfilDto dto)
		{
			var perfil = await _perfilRepository.GetByIdWithFuncionalidadesAsync(id);
			if (perfil == null) return Result.Failure(new[] { "Perfil não encontrado." });

			var existenteComNome = await _perfilRepository.GetByNameAsync(dto.Nome);
			if (existenteComNome != null && existenteComNome.Id != id)
			{
				return Result.Failure(new[] { $"Já existe outro perfil com o nome '{dto.Nome}	'." });
			}

			perfil.Nome = dto.Nome;
			perfil.Descricao = dto.Descricao;

			// Atualizar funcionalidades (exemplo simples: substitui todas)
			if (dto.FuncionalidadeIds != null)
			{
				perfil.Funcionalidades.Clear();
				foreach (var funcId in dto.FuncionalidadeIds)
				{
					var funcionalidade = await _funcionalidadeRepository.GetByIdAsync(funcId);
					if (funcionalidade != null)
					{
						perfil.Funcionalidades.Add(funcionalidade);
					}
				}
			}

			await _perfilRepository.UpdateAsync(perfil);
			var resultDto = MapToDto(perfil);
			return Result<PerfilDto>.Success(resultDto);
			// return Result.Success();
		}

		public async Task<Result> DeleteAsync(Guid id)
		{
			var perfil = await _perfilRepository.GetByIdAsync(id);
			if (perfil == null) return Result.Failure(new[] { "Perfil não encontrado." });

			// Adicionar validação: verificar se o perfil está em uso por algum usuário antes de excluir?

			await _perfilRepository.DeleteAsync(id);
			return Result.Success();
		}

		public async Task<Result> AddFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId)
		{
			var perfil = await _perfilRepository.GetByIdAsync(perfilId);
			var funcionalidade = await _funcionalidadeRepository.GetByIdAsync(funcionalidadeId);

			if (perfil == null) return Result.Failure(new[] { "Perfil não encontrado." });
			if (funcionalidade == null) return Result.Failure(new[] { "Funcionalidade não encontrada." });

			await _perfilRepository.AddFuncionalidadeAsync(perfilId, funcionalidadeId);
			return Result.Success();
		}

		public async Task<Result> RemoveFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId)
		{
			// Validações podem ser feitas aqui ou no repositório
			await _perfilRepository.RemoveFuncionalidadeAsync(perfilId, funcionalidadeId);
			return Result.Success();
		}

		// Método auxiliar de mapeamento
		private PerfilDto MapToDto(Perfil perfil)
		{
			return new PerfilDto
			{
				Id = perfil.Id,
				Nome = perfil.Nome,
				Descricao = perfil.Descricao,
				Funcionalidades = perfil.Funcionalidades? // Mapeia funcionalidades se carregadas
					.Select(f => new FuncionalidadeDto { Id = f.Id, Nome = f.Nome, Descricao = f.Descricao })
					.ToList()
			};
		}
	}
}
