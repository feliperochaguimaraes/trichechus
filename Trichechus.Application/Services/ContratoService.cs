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
		var contrato = await _repository.GetAllAsync();
		return contrato.Select(a => MapToDTO(a));
	}

	public async Task<Result<ContratoDto>> GetContratoByIdAsync(Guid id)
	{
		var contrato = await _repository.GetByIdAsync(id);
		if (contrato == null)
		{
			return Result<ContratoDto>.Failure(new List<string> { "Contrato não encontrado." });
		}

		return Result<ContratoDto>.Success(MapToDTO(contrato));
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

		var contrato = new Contrato
		{
			NomeAlias = dto.NomeAlias,
			Numero = dto.Numero,
			Objeto = dto.Objeto,
			Ativo = dto.Ativo,
			Inicio = dto.Inicio,
			Fim = dto.Fim,
			AreaGestora = dto.AreaGestora,
			Gerencia = dto.Gerencia


		};

		await _repository.AddAsync(contrato);
		return Result<Guid>.Success(contrato.Id);
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

		var contrato = await _repository.GetByIdAsync(dto.Id);
		if (contrato == null)
		{
			return Result.Failure(new List<string> { "Contrato não encontrada." });
		}

		contrato.NomeAlias = dto.NomeAlias;
		contrato.Numero = dto.Numero;
		contrato.Objeto = dto.Objeto;
		contrato.Ativo = dto.Ativo;
		contrato.Inicio = dto.Inicio;
		contrato.Fim = dto.Fim;
		contrato.AreaGestora = dto.AreaGestora;
		contrato.Gerencia = dto.Gerencia;
		await _repository.UpdateAsync(contrato);
		return Result.Success();
	}

	public async Task<Result> DeleteContratoAsync(Guid id)
	{
		var contrato = await _repository.GetByIdAsync(id);
		if (contrato == null)
		{
			return Result.Failure(new List<string> { "Contrato não encontrada." });
		}

		
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	private ContratoDto MapToDTO(Contrato contrato)
	{
		return new ContratoDto
		{
			Id = contrato.Id,
			NomeAlias = contrato.NomeAlias,
			Numero = contrato.Numero,
			Objeto = contrato.Objeto,
			Ativo = contrato.Ativo,
			Inicio = contrato.Inicio,
			Fim = contrato.Fim,
			AreaGestora = contrato.AreaGestora,
			Gerencia = contrato.Gerencia
			
		};
	}
}
