using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class RepositorioService : IRepositorioService
{
	private readonly IRepositorioRepository _repository;
	private readonly IValidator<CreateRepositorioDto> _createValidator;
	private readonly IValidator<UpdateRepositorioDto> _updateValidator;
	private readonly IUserContext _userContext;

	public RepositorioService(
		IRepositorioRepository repository,
		IValidator<CreateRepositorioDto> createValidator,
		IValidator<UpdateRepositorioDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<RepositorioDto>> GetAllRepositorioAsync()
	{
		var repositorio = await _repository.GetAllAsync();
		return repositorio.Select(a => MapToDTO(a));
	}

	public async Task<Result<RepositorioDto>> GetRepositorioByIdAsync(Guid id)
	{
		var repositorio = await _repository.GetByIdAsync(id);
		if (repositorio == null)
		{
			return Result<RepositorioDto>.Failure(new List<string> { "Repositorio não encontrada." });
		}

		return Result<RepositorioDto>.Success(MapToDTO(repositorio));
	}
	public async Task<Result<Guid>> CreateRepositorioAsync(CreateRepositorioDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var repositorio = new Repositorio
		{
			Endereco = dto.Endereco,
			Tipo = dto.Tipo
		};

		await _repository.AddAsync(repositorio);
		return Result<Guid>.Success(repositorio.Id);
	}

	public async Task<Result> UpdateRepositorioAsync(UpdateRepositorioDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var repositorio = await _repository.GetByIdAsync(dto.Id);
		if (repositorio == null)
		{
			return Result.Failure(new List<string> { "Repositorio não encontrada." });
		}

		repositorio.Endereco = dto.Endereco;
		repositorio.Tipo = dto.Tipo;

		await _repository.UpdateAsync(repositorio);
		return Result.Success();
	}

	public async Task<Result> DeleteRepositorioAsync(Guid id)
	{
		var repositorio = await _repository.GetByIdAsync(id);
		if (repositorio == null)
		{
			return Result.Failure(new List<string> { "Repositorio não encontrada." });
		}

		// Soft delete
		// repositorio.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(repositorio);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftRepositorioAsync(Guid id)
	{
		var repositorio = await _repository.GetByIdAsync(id);
		if (repositorio == null)
		{
			return Result.Failure(new List<string> { "Repositorio não encontrada." });
		}
		
		await _repository.UpdateAsync(repositorio);
		return Result.Success();
	}

	private RepositorioDto MapToDTO(Repositorio repositorio)
	{
		return new RepositorioDto
		{
			Id = repositorio.Id,
			Endereco = repositorio.Endereco,
			Tipo = repositorio.Tipo
		};
	}
}
