

using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class URLService : IURLService
{
	private readonly IURLRepository _UrlRepository;
	private readonly ISoftwareRepository _SoftwareRepository;
	private readonly IValidator<CreateUrlDto> _createValidator;
	private readonly IValidator<UpdateUrlDto> _updateValidator;
	private readonly IUserContext _userContext;

	public URLService(
		IURLRepository urlRepository,
		ISoftwareRepository softwareRepository,
		IValidator<CreateUrlDto> createValidator,
		IValidator<UpdateUrlDto> updateValidator,
		IUserContext userContext
		)
	{
		_UrlRepository = urlRepository;
		_SoftwareRepository = softwareRepository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}

	public async Task<Result<UrlDto>> GetByIdAsync(Guid id)
	{
		var url = await _UrlRepository.GetByIdWithSoftwareAsync(id);
		if (url == null)
		{
			return Result<UrlDto>.Failure(new[] { "Software não encontrado." });
		}

		var dto = MapToDTO(url);
		return Result<UrlDto>.Success(dto);
	}

	public async Task<Result<IEnumerable<UrlDto>>> GetAllUrlAsync()
	{
		var url = await _UrlRepository.GetAllUrlAsync();

		var dtos = url.Select(a => MapToDTO(a));

		return Result<IEnumerable<UrlDto>>.Success(dtos);
	}

	public async Task<Result<UrlDto>> GetUrlByIdAsync(Guid id)
	{
		var url = await _UrlRepository.GetByIdWithSoftwareAsync(id);
		if (url == null)
		{
			return Result<UrlDto>.Failure(new List<string> { "Url não encontrada." });
		}

		return Result<UrlDto>.Success(MapToDTO(url));
	}

	public async Task<Result<Guid>> CreateUrlAsync(CreateUrlDto dto)
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

		await _UrlRepository.AddAsync(url);
		return Result<Guid>.Success(url.Id);
	}

	public async Task<Result> UpdateUrlAsync(UpdateUrlDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var url = await _UrlRepository.GetByIdAsync(dto.Id);
		if (url == null)
		{
			return Result.Failure(new List<string> { "Url não encontrada." });
		}

		url.Endereco = dto.Endereco;
		url.Ambiente = dto.Ambiente;
		url.Servidor = dto.Servidor;
		url.IP = dto.IP;

		await _UrlRepository.UpdateAsync(url);
		var resultDto = MapToDTO(url);
		return Result<UrlDto>.Success(resultDto);
	}

	public async Task<Result> DeleteUrlAsync(Guid id)
	{
		var url = await _UrlRepository.GetByIdAsync(id);
		if (url == null)
		{
			return Result.Failure(new List<string> { "Url não encontrada." });
		}

		await _UrlRepository.DeleteAsync(id);
		return Result.Success();
	}

	private UrlDto MapToDTO(URL url)
	{
		return new UrlDto
		{
			Id = url.Id,
			Endereco = url.Endereco,
			Ambiente = url.Ambiente,
			Servidor = url.Servidor,
			IP = url.IP
		};
	}
}
