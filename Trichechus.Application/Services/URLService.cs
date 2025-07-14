using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class URLService : IURLService
{
	private readonly IURLRepository _repository;
	private readonly IValidator<CreateURLDto> _createValidator;
	private readonly IValidator<UpdateURLDto> _updateValidator;
	private readonly IUserContext _userContext;

	public URLService(
		IURLRepository repository,
		IValidator<CreateURLDto> createValidator,
		IValidator<UpdateURLDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<URLDto>> GetAllURLAsync()
	{
		var url = await _repository.GetAllAsync();
		return url.Select(a => MapToDTO(a));
	}

	public async Task<Result<URLDto>> GetURLByIdAsync(Guid id)
	{
		var url = await _repository.GetByIdAsync(id);
		if (url == null)
		{
			return Result<URLDto>.Failure(new List<string> { "URL não encontrada." });
		}

		return Result<URLDto>.Success(MapToDTO(url));
	}
	public async Task<Result<Guid>> CreateURLAsync(CreateURLDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var url = new URL
		{
			Endereco = dto.Endereco,
			Ambiente = dto.Ambiente,
			Servidor = dto.Servidor,
			IP = dto.IP
		};

		await _repository.AddAsync(url);
		return Result<Guid>.Success(url.Id);
	}

	public async Task<Result> UpdateURLAsync(UpdateURLDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var url = await _repository.GetByIdAsync(dto.Id);
		if (url == null)
		{
			return Result.Failure(new List<string> { "URL não encontrada." });
		}

		url.Endereco = dto.Endereco;
		url.Ambiente = dto.Ambiente;
		url.Servidor = dto.Servidor;
		url.IP = dto.IP;

		await _repository.UpdateAsync(url);
		return Result.Success();
	}

	public async Task<Result> DeleteURLAsync(Guid id)
	{
		var url = await _repository.GetByIdAsync(id);
		if (url == null)
		{
			return Result.Failure(new List<string> { "URL não encontrada." });
		}

		// Soft delete
		// url.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(url);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftURLAsync(Guid id)
	{
		var url = await _repository.GetByIdAsync(id);
		if (url == null)
		{
			return Result.Failure(new List<string> { "URL não encontrada." });
		}
		
		await _repository.UpdateAsync(url);
		return Result.Success();
	}

	private URLDto MapToDTO(URL url)
	{
		return new URLDto
		{
			Id = url.Id,
			Endereco = url.Endereco,
			Ambiente = url.Ambiente,
			Servidor = url.Servidor,
			IP = url.IP
		};
	}
}
