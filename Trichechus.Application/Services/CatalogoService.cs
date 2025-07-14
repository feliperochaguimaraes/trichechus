using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class CatalogoService : ICatalogoService
{
	private readonly ICatalogoRepository _repository;
	private readonly IValidator<CreateCatalogoDto> _createValidator;
	private readonly IValidator<UpdateCatalogoDto> _updateValidator;
	private readonly IUserContext _userContext;

	public CatalogoService(
		ICatalogoRepository repository,
		IValidator<CreateCatalogoDto> createValidator,
		IValidator<UpdateCatalogoDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<CatalogoDto>> GetAllCatalogoAsync()
	{
		var catalogo = await _repository.GetAllAsync();
		return catalogo.Select(a => MapToDTO(a));
	}

	public async Task<Result<CatalogoDto>> GetCatalogoByIdAsync(Guid id)
	{
		var catalogo = await _repository.GetByIdAsync(id);
		if (catalogo == null)
		{
			return Result<CatalogoDto>.Failure(new List<string> { "Catalogo não encontrada." });
		}

		return Result<CatalogoDto>.Success(MapToDTO(catalogo));
	}
	public async Task<Result<Guid>> CreateCatalogoAsync(CreateCatalogoDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var catalogo = new Catalogo
		{
			HelixId = dto.HelixId,
			HelixEquipe = dto.HelixEquipe,
			HelixService = dto.HelixService,
			HelixCategoria = dto.HelixCategoria,
			HelixSubcategoria = dto.HelixSubcategoria,
			CatalogoEquipe = dto.CatalogoEquipe,
			Ativo = dto.Ativo,
			Observacao = dto.Observacao
		};

		await _repository.AddAsync(catalogo);
		return Result<Guid>.Success(catalogo.Id);
	}

	public async Task<Result> UpdateCatalogoAsync(UpdateCatalogoDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var catalogo = await _repository.GetByIdAsync(dto.Id);
		if (catalogo == null)
		{
			return Result.Failure(new List<string> { "Catalogo não encontrada." });
		}

		catalogo.HelixId = dto.HelixId;
		catalogo.HelixEquipe = dto.HelixEquipe;
		catalogo.HelixService = dto.HelixService;
		catalogo.HelixCategoria = dto.HelixCategoria;
		catalogo.HelixSubcategoria = dto.HelixSubcategoria;
		catalogo.CatalogoEquipe = dto.CatalogoEquipe;
		catalogo.Ativo = dto.Ativo;
		catalogo.Observacao = dto.Observacao;

		await _repository.UpdateAsync(catalogo);
		return Result.Success();
	}

	public async Task<Result> DeleteCatalogoAsync(Guid id)
	{
		var catalogo = await _repository.GetByIdAsync(id);
		if (catalogo == null)
		{
			return Result.Failure(new List<string> { "Catalogo não encontrada." });
		}

		// Soft delete
		// catalogo.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(catalogo);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftCatalogoAsync(Guid id)
	{
		var catalogo = await _repository.GetByIdAsync(id);
		if (catalogo == null)
		{
			return Result.Failure(new List<string> { "Catalogo não encontrada." });
		}
		
		await _repository.UpdateAsync(catalogo);
		return Result.Success();
	}

	private CatalogoDto MapToDTO(Catalogo catalogo)
	{
		return new CatalogoDto
		{
			Id = catalogo.Id,
			HelixId = catalogo.HelixId,
			HelixEquipe = catalogo.HelixEquipe,
			HelixService = catalogo.HelixService,
			HelixCategoria = catalogo.HelixCategoria,
			HelixSubcategoria = catalogo.HelixSubcategoria,
			CatalogoEquipe = catalogo.CatalogoEquipe,
			Ativo = catalogo.Ativo,
			Observacao = catalogo.Observacao
		};
	}
}
