using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class CatalogoService : ICatalogoService
{
	private readonly ICatalogoRepository _CatalogoRepository;
	private readonly ISoftwareRepository _SoftwareRepository;
	private readonly IValidator<CreateCatalogoDto> _createValidator;
	private readonly IValidator<UpdateCatalogoDto> _updateValidator;
	private readonly IUserContext _userContext;

	public CatalogoService(
		ICatalogoRepository catalogoRepository,
		ISoftwareRepository softwareRepository,
		IValidator<CreateCatalogoDto> createValidator,
		IValidator<UpdateCatalogoDto> updateValidator,
		IUserContext userContext

		)
	{
		_CatalogoRepository = catalogoRepository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
		_SoftwareRepository = softwareRepository;
	}
	public async Task<Result<CatalogoDto>> GetByIdAsync(Guid id)
	{
		var catalogo = await _CatalogoRepository.GetByIdWithSoftwareAsync(id);
		if (catalogo == null)
		{
			return Result<CatalogoDto>.Failure(new[] { "Software não encontrado." });
		}

		var dto = MapToDTO(catalogo);
		return Result<CatalogoDto>.Success(dto);
	}
	
	public async Task<Result<IEnumerable<CatalogoDto>>> GetAllCatalogoAsync()
	{
		var catalogo = await _CatalogoRepository.GetAllCatalogoAsync();

		var dtos = catalogo.Select(a => MapToDTO(a));

		return Result<IEnumerable<CatalogoDto>>.Success(dtos);
	}

	public async Task<Result<CatalogoDto>> GetCatalogoByIdAsync(Guid id)
	{
		var catalogo = await _CatalogoRepository.GetByIdWithSoftwareAsync(id);
		if (catalogo == null)
		{
			return Result<CatalogoDto>.Failure(new List<string> { "BaseDados não encontrada." });
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

		
		await _CatalogoRepository.AddAsync(catalogo);
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

		var catalogo = await _CatalogoRepository.GetByIdWithSoftwareAsync(dto.Id);
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

		
		await _CatalogoRepository.UpdateAsync(catalogo);
		var resultDto = MapToDTO(catalogo);
		return Result<CatalogoDto>.Success(resultDto);
	}

	public async Task<Result> DeleteCatalogoAsync(Guid id)
	{
		// var catalogo = await _repository.GetByIdAsync(id);
		// if (catalogo == null)
		// {
		// 	return Result.Failure(new List<string> { "Catalogo não encontrada." });
		// }

		// // Soft delete
		// // catalogo.DeletadoEm = DateTime.Now;
		// // await _repository.UpdateAsync(catalogo);

		// // Ou hard delete
		// await _repository.DeleteAsync(id);
		// return Result.Success();
		var catalogo = await _CatalogoRepository.GetByIdAsync(id);
		if (catalogo == null) return Result.Failure(new[] { "Catalogo não encontrado." });

		await _CatalogoRepository.DeleteAsync(id);
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
