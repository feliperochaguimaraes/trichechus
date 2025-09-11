using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class GerenciaService : IGerenciaService
{
	private readonly IGerenciaRepository _repository;
	private readonly IValidator<CreateGerenciaDto> _createValidator;
	private readonly IValidator<UpdateGerenciaDto> _updateValidator;
	private readonly IUserContext _userContext;

	public GerenciaService(
		IGerenciaRepository repository,
		IValidator<CreateGerenciaDto> createValidator,
		IValidator<UpdateGerenciaDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	
	public async Task<Result<GerenciaDto>> GetByIdAsync(Guid id)
	{
		var gerencia = await _repository.GetByIdAsync(id);
		if (gerencia == null)
		{
			return Result<GerenciaDto>.Failure(new[] { "Gerencia não encontrado." });
		}

		var dto = MapToDto(gerencia);
		return Result<GerenciaDto>.Success(dto);
	}

	public async Task<IEnumerable<GerenciaDto>> GetAllAsync()
	{
		var gerencia = await _repository.GetAllAsync();
		return gerencia.Select(a => MapToDto(a));
	}

	public async Task<Result<Guid>> CreateAsync(CreateGerenciaDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var gerencia = new Gerencia
		{
			NomeGerencia = dto.NomeGerencia,
			Superintendencia = dto.Superintendencia,
		};

		await _repository.AddAsync(gerencia);
		return Result<Guid>.Success(gerencia.Id);
	}

	public async Task<Result> UpdateAsync(UpdateGerenciaDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var gerencia = await _repository.GetByIdAsync(dto.Id);
		if (gerencia == null)
		{
			return Result.Failure(new List<string> { "Gerencia não encontrada." });
		}

		gerencia.NomeGerencia = dto.NomeGerencia;
		gerencia.Superintendencia = dto.Superintendencia;

		await _repository.UpdateAsync(gerencia);
		return Result.Success();
	}

	public async Task<Result> DeleteAsync(Guid id)
	{
		var gerencia = await _repository.GetByIdAsync(id);
		if (gerencia == null)
		{
			return Result.Failure(new List<string> { "Gerencia não encontrada." });
		}

		// Soft delete
		// gerencia.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(gerencia);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	private GerenciaDto MapToDto(Gerencia gerencia)
	{
		return new GerenciaDto
		{
			Id = gerencia.Id,
			NomeGerencia = gerencia.NomeGerencia,
			Superintendencia = gerencia.Superintendencia
		};
	}
}
