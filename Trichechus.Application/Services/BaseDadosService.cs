using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class BaseDadosService : IBaseDadosService
{
	private readonly IBaseDadosRepository _BaseDadosRepository;
	private readonly ISoftwareRepository _SoftwareRepository;
	private readonly IValidator<CreateBaseDadosDto> _createValidator;
	private readonly IValidator<UpdateBaseDadosDto> _updateValidator;
	private readonly IUserContext _userContext;

	public BaseDadosService(
		IBaseDadosRepository baseDadosRepository,
		ISoftwareRepository softwareRepository,
		IValidator<CreateBaseDadosDto> createValidator,
		IValidator<UpdateBaseDadosDto> updateValidator,
		IUserContext userContext
		)
	{
		_BaseDadosRepository = baseDadosRepository;
		_SoftwareRepository = softwareRepository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<Result<BaseDadosDto>> GetByIdAsync(Guid id)
	{
		var baseDados = await _BaseDadosRepository.GetByIdWithSoftwareAsync(id);
		if (baseDados == null)
		{
			return Result<BaseDadosDto>.Failure(new[] { "Software não encontrado." });
		}

		var dto = MapToDTO(baseDados);
		return Result<BaseDadosDto>.Success(dto);
	}
	
	public async Task<Result<IEnumerable<BaseDadosDto>>> GetAllBaseDadosAsync()
	{
		var baseDados = await _BaseDadosRepository.GetAllBaseDadosAsync();

		var dtos = baseDados.Select(a => MapToDTO(a));

		return Result<IEnumerable<BaseDadosDto>>.Success(dtos);
	}

	public async Task<Result<BaseDadosDto>> GetBaseDadosByIdAsync(Guid id)
	{
		var basedados = await _BaseDadosRepository.GetByIdWithSoftwareAsync(id);
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

		basedados.Software ??= new List<Software>();

		if (dto.SoftwareIds != null)
		{
			foreach (var softId in dto.SoftwareIds)
			{
				var software = await _SoftwareRepository.GetByIdAsync(softId);
				if (software != null)
				{
					basedados.Software.Add(software);
				}
			}
		}
		await _BaseDadosRepository.AddAsync(basedados);
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

		var basedados = await _BaseDadosRepository.GetByIdAsync(dto.Id);
		if (basedados == null)
		{
			return Result.Failure(new List<string> { "BaseDados não encontrada." });
		}

		basedados.Cluster = dto.Cluster;
		basedados.NomeBaseDados = dto.NomeBaseDados;
		basedados.Versao = dto.Versao;

		// Atualizar Base de Dados (exemplo simples: substitui todas)
		if (dto.SoftwareIds != null)
		{
			basedados.Software.Clear();
			foreach (var softId in dto.SoftwareIds)
			{
				var software = await _SoftwareRepository.GetByIdAsync(softId);
				if (software != null)
				{
					basedados.Software.Add(software);
				}
			}
		}

		await _BaseDadosRepository.UpdateAsync(basedados);
		var resultDto = MapToDTO(basedados);
		return Result<BaseDadosDto>.Success(resultDto);
	}

	public async Task<Result> DeleteBaseDadosAsync(Guid id)
	{
		var basedados = await _BaseDadosRepository.GetByIdAsync(id);
		if (basedados == null)
		{
			return Result.Failure(new List<string> { "BaseDados não encontrada." });
		}

		// Soft delete
		// basedados.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(basedados);

		// Ou hard delete
		await _BaseDadosRepository.DeleteAsync(id);
		return Result.Success();
	}
	public async Task<Result> AddSoftBaseDadosAsync(Guid baseDadosId, Guid softwareId)
	{
		var baseDados = await _BaseDadosRepository.GetByIdAsync(baseDadosId);
		var software = await _SoftwareRepository.GetByIdAsync(softwareId);

		if (baseDados == null) return Result.Failure(new[] { "Base de dados não encontrado." });
		if (software == null) return Result.Failure(new[] { "Software não encontrado." });

		await _BaseDadosRepository.AddSoftAsync(softwareId, baseDadosId);
		return Result.Success();
	}

	public async Task<Result> RemoveSoftBaseDadosAsync(Guid baseDadosId, Guid softwareId)
	{
		// Validações podem ser feitas aqui ou no repositório
		await _BaseDadosRepository.RemoveSoftBaseDadosAsync(baseDadosId, softwareId);
		return Result.Success();
	}

	// public async Task<Result> RemoveSoftBaseDadosAsync(Guid id)
	// {
	// 	var basedados = await _BaseDadosRepository.GetByIdAsync(id);
	// 	if (basedados == null)
	// 	{
	// 		return Result.Failure(new List<string> { "BaseDados não encontrada." });
	// 	}

	// 	await _BaseDadosRepository.UpdateAsync(basedados);
	// 	return Result.Success();
	// }


	private BaseDadosDto MapToDTO(BaseDados basedados)
	{
		return new BaseDadosDto
		{
			Id = basedados.Id,
			Cluster = basedados.Cluster,
			NomeBaseDados = basedados.NomeBaseDados,
			Versao = basedados.Versao,
			Softwares = basedados.Software? // Mapeia contratos para serem carregados
				.Select(f => new SoftwareDto { Id = f.Id, Nome = f.Nome, Situacao = f.Situacao })
				.ToList()

		};
	}

}
