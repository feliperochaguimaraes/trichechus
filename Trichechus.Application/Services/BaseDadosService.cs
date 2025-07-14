using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class BaseDadosService : IBaseDadosService
{
	private readonly IBaseDadosRepository _repository;
	private readonly IValidator<CreateBaseDadosDto> _createValidator;
	private readonly IValidator<UpdateBaseDadosDto> _updateValidator;
	private readonly IUserContext _userContext;

	public BaseDadosService(
		IBaseDadosRepository repository,
		IValidator<CreateBaseDadosDto> createValidator,
		IValidator<UpdateBaseDadosDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<BaseDadosDto>> GetAllBaseDadosAsync()
	{
		var basedados = await _repository.GetAllAsync();
		return basedados.Select(a => MapToDTO(a));
	}

	public async Task<Result<BaseDadosDto>> GetBaseDadosByIdAsync(Guid id)
	{
		var basedados = await _repository.GetByIdAsync(id);
		if (basedados == null)
		{
			return Result<BaseDadosDto>.Failure(new List<string> { "BaseDados não encontrada." });
		}

		return Result<BaseDadosDto>.Success(MapToDTO(basedados));
	}
	public async Task<Result<Guid>> CreateBaseDadosAsync(CreateBaseDadosDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var basedados = new BaseDados
		{
			Cluster = dto.Cluster,
			NomeBaseDados = dto.NomeBaseDados,
			Versao = dto.Versao
		};

		await _repository.AddAsync(basedados);
		return Result<Guid>.Success(basedados.Id);
	}

	public async Task<Result> UpdateBaseDadosAsync(UpdateBaseDadosDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var basedados = await _repository.GetByIdAsync(dto.Id);
		if (basedados == null)
		{
			return Result.Failure(new List<string> { "BaseDados não encontrada." });
		}

		basedados.Cluster = dto.Cluster;
		basedados.NomeBaseDados = dto.NomeBaseDados;
		basedados.Versao = dto.Versao;

		await _repository.UpdateAsync(basedados);
		return Result.Success();
	}

	public async Task<Result> DeleteBaseDadosAsync(Guid id)
	{
		var basedados = await _repository.GetByIdAsync(id);
		if (basedados == null)
		{
			return Result.Failure(new List<string> { "BaseDados não encontrada." });
		}

		// Soft delete
		// basedados.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(basedados);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftBaseDadosAsync(Guid id)
	{
		var basedados = await _repository.GetByIdAsync(id);
		if (basedados == null)
		{
			return Result.Failure(new List<string> { "BaseDados não encontrada." });
		}

		await _repository.UpdateAsync(basedados);
		return Result.Success();
	}

	private BaseDadosDto MapToDTO(BaseDados basedados)
	{
		return new BaseDadosDto
		{
			Id = basedados.Id,
			Cluster = basedados.Cluster,
			NomeBaseDados = basedados.NomeBaseDados,
			Versao = basedados.Versao
		};
	}
}
