using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class FuncionalidadeService : IFuncionalidadeService
{
	private readonly IFuncionalidadeRepository _repository;
	public FuncionalidadeService(IFuncionalidadeRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<FuncionalidadeDto>> GetFuncionalidadeByIdAsync(Guid id)
	{
		var funcionalidade = await _repository.GetByIdAsync(id);
		if (funcionalidade == null) return Result<FuncionalidadeDto>.Failure(new[] { "Funcionalidade não encontrada." });

		var dto = new FuncionalidadeDto { Id = funcionalidade.Id, Nome = funcionalidade.Nome, Descricao = funcionalidade.Descricao };
		return Result<FuncionalidadeDto>.Success(dto);
	}

	public async Task<Result<IEnumerable<FuncionalidadeDto>>> GetAllFuncionalidadeAsync()
	{
		var funcionalidades = await _repository.GetAllAsync();
		var dtos = funcionalidades.Select(f => new FuncionalidadeDto { Id = f.Id, Nome = f.Nome, Descricao = f.Descricao });
		return Result<IEnumerable<FuncionalidadeDto>>.Success(dtos);
	}

	public async Task<Result<FuncionalidadeDto>> CreateFuncionalidadeAsync(CreateFuncionalidadeDto dto)
	{
		// Validar se já existe uma funcionalidade com o mesmo nome
		if (await _repository.GetByNameAsync(dto.Nome) != null)
		{
			return Result<FuncionalidadeDto>.Failure(new[] { $"Já existe uma funcionalidade com o nome 	'{dto.Nome}	'." });
		}

		var funcionalidade = new Funcionalidade
		{
			Nome = dto.Nome,
			Descricao = dto.Descricao
		};

		await _repository.AddAsync(funcionalidade);

		var resultDto = new FuncionalidadeDto { Id = funcionalidade.Id, Nome = funcionalidade.Nome, Descricao = funcionalidade.Descricao };
		return Result<FuncionalidadeDto>.Success(resultDto);
	}

	public async Task<Result> UpdateFuncionalidadeAsync(Guid id, UpdateFuncionalidadeDto dto)
	{
		var funcionalidade = await _repository.GetByIdAsync(id);
		if (funcionalidade == null) return Result.Failure(new[] { "Funcionalidade não encontrada." });

		// Validar se o novo nome já existe em outra funcionalidade
		var existenteComNome = await _repository.GetByNameAsync(dto.Nome);
		if (existenteComNome != null && existenteComNome.Id != id)
		{
			return Result.Failure(new[] { $"Já existe outra funcionalidade com o nome 	'{dto.Nome}	'." });
		}

		funcionalidade.Nome = dto.Nome;
		funcionalidade.Descricao = dto.Descricao;

		await _repository.UpdateAsync(funcionalidade);
		return Result.Success();
	}
	
	public async Task<Result> DeleteFuncionalidadeAsync(Guid id)
	{
		var funcionalidade = await _repository.GetByIdAsync(id);
		if (funcionalidade == null) return Result.Failure(new[] { "Funcionalidade não encontrada." });

		// Adicionar validação aqui: verificar se a funcionalidade está em uso por algum perfil antes de excluir?

		await _repository.DeleteAsync(id);
		return Result.Success();
	}
}