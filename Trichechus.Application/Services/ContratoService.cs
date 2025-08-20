using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class ContratoService : IContratoService
{
	private readonly IContratoRepository _repository;
	private readonly IValidator<CreateContratoDto> _createValidator;
	private readonly IValidator<UpdateContratoDto> _updateValidator;
	private readonly IUserContext _userContext;

	public ContratoService(
		IContratoRepository repository,
		IValidator<CreateContratoDto> createValidator,
		IValidator<UpdateContratoDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<ContratoDto>> GetAllContratoAsync()
	{
		var fornecedor = await _repository.GetAllAsync();
		return fornecedor.Select(a => MapToDTO(a));
	}

	public async Task<Result<ContratoDto>> GetContratoByIdAsync(Guid id)
	{
		var fornecedor = await _repository.GetByIdAsync(id);
		if (fornecedor == null)
		{
			return Result<ContratoDto>.Failure(new List<string> { "Contrato não encontrada." });
		}

		return Result<ContratoDto>.Success(MapToDTO(fornecedor));
	}
	public async Task<Result<Guid>> CreateContratoAsync(CreateContratoDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var fornecedor = new Contrato
		{
			NomeAlias = dto.NomeAlias,
			Numero = dto.Numero,
			Objeto = dto.Objeto,
			Ativo = dto.Ativo,
			Inicio = dto.Inicio,
			Fim = dto.Fim
		};

		await _repository.AddAsync(fornecedor);
		return Result<Guid>.Success(fornecedor.Id);
	}

	public async Task<Result> UpdateContratoAsync(UpdateContratoDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var fornecedor = await _repository.GetByIdAsync(dto.Id);
		if (fornecedor == null)
		{
			return Result.Failure(new List<string> { "Contrato não encontrada." });
		}

		fornecedor.NomeAlias = dto.NomeAlias;
		fornecedor.Objeto = dto.Objeto;
		fornecedor.Ativo = dto.Ativo;
		fornecedor.Inicio = dto.Inicio;
		fornecedor.Fim = dto.Fim;

		await _repository.UpdateAsync(fornecedor);
		return Result.Success();
	}

	public async Task<Result> DeleteContratoAsync(Guid id)
	{
		var fornecedor = await _repository.GetByIdAsync(id);
		if (fornecedor == null)
		{
			return Result.Failure(new List<string> { "Contrato não encontrada." });
		}

		// Soft delete
		// fornecedor.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(fornecedor);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftContratoAsync(Guid id)
	{
		var fornecedor = await _repository.GetByIdAsync(id);
		if (fornecedor == null)
		{
			return Result.Failure(new List<string> { "Contrato não encontrada." });
		}

		await _repository.UpdateAsync(fornecedor);
		return Result.Success();
	}

	private ContratoDto MapToDTO(Contrato fornecedor)
	{
		return new ContratoDto
		{
			Id = fornecedor.Id,
			NomeAlias = fornecedor.NomeAlias,
			Objeto = fornecedor.Objeto,
			Inicio = fornecedor.Inicio,
			Fim = fornecedor.Fim,
			Ativo = fornecedor.Ativo
		};
	}
}
