using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class SoftwareService : ISoftwareService
{
	private readonly ISoftwareRepository _repository;
	private readonly IValidator<CreateSoftwareDto> _createValidator;
	private readonly IValidator<UpdateSoftwareDto> _updateValidator;
	private readonly IUserContext _userContext;

	public SoftwareService(
		ISoftwareRepository repository,
		IValidator<CreateSoftwareDto> createValidator,
		IValidator<UpdateSoftwareDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<SoftwareDto>> GetAllSoftwareAsync()
	{
		var software = await _repository.GetAllAsync();
		return software.Select(a => MapToDTO(a));
	}

	public async Task<Result<SoftwareDto>> GetSoftwareByIdAsync(Guid id)
	{
		var software = await _repository.GetByIdAsync(id);
		if (software == null)
		{
			return Result<SoftwareDto>.Failure(new List<string> { "Software não encontrada." });
		}

		return Result<SoftwareDto>.Success(MapToDTO(software));
	}
	public async Task<Result<Guid>> CreateSoftwareAsync(CreateSoftwareDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var software = new Software
		{
			ProdutoSoftware = dto.ProdutoSoftware,
			Nome = dto.Nome,
			Situacao = dto.Situacao,
			Descricao = dto.Descricao,
			Segmento = dto.Segmento,
			Tipo = dto.Tipo,
			LicencaSoftware = dto.LicencaSoftware,
			Tecnologia = dto.Tecnologia,
			Descontinuado = dto.Descontinuado,
			EntrarCatalogo = dto.EntrarCatalogo
		};

		await _repository.AddAsync(software);
		return Result<Guid>.Success(software.Id);
	}

	public async Task<Result> UpdateSoftwareAsync(UpdateSoftwareDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var software = await _repository.GetByIdAsync(dto.Id);
		if (software == null)
		{
			return Result.Failure(new List<string> { "Software não encontrada." });
		}

		software.ProdutoSoftware = dto.ProdutoSoftware;
		software.Nome = dto.Nome;
		software.Situacao = dto.Situacao;
		software.Descricao = dto.Descricao;
		software.Segmento = dto.Segmento;
		software.Tipo = dto.Tipo;
		software.LicencaSoftware = dto.LicencaSoftware;
		software.Tecnologia = dto.Tecnologia;
		software.Descontinuado = dto.Descontinuado;
		software.EntrarCatalogo = dto.EntrarCatalogo;

		await _repository.UpdateAsync(software);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftwareAsync(Guid id)
	{
		var software = await _repository.GetByIdAsync(id);
		if (software == null)
		{
			return Result.Failure(new List<string> { "Software não encontrada." });
		}

		// Soft delete
		// software.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(software);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftSoftwareAsync(Guid id)
	{
		var software = await _repository.GetByIdAsync(id);
		if (software == null)
		{
			return Result.Failure(new List<string> { "Software não encontrada." });
		}
		
		await _repository.UpdateAsync(software);
		return Result.Success();
	}

	private SoftwareDto MapToDTO(Software software)
	{
		return new SoftwareDto
		{
			Id = software.Id,
			ProdutoSoftware = software.ProdutoSoftware,
			Nome = software.Nome,
			Situacao = software.Situacao,
			Descricao = software.Descricao,
			Segmento = software.Segmento,
			Tipo = software.Tipo,
			LicencaSoftware = software.LicencaSoftware,
			Tecnologia = software.Tecnologia,
			Descontinuado = software.Descontinuado,
			EntrarCatalogo = software.EntrarCatalogo
		};
	}
}
