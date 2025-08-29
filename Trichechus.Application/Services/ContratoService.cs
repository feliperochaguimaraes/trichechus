using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class ContratoService : IContratoService
{
	private readonly IContratoRepository _ContratoRepository;
	private readonly IFornecedorRepository _FornecedorRepository;
	private readonly IValidator<CreateContratoDto> _createValidator;
	private readonly IValidator<UpdateContratoDto> _updateValidator;
	private readonly IUserContext _userContext;

	public ContratoService(
		IContratoRepository contratoRepository,
		IFornecedorRepository fornecedorRepository,
		IValidator<CreateContratoDto> createValidator,
		IValidator<UpdateContratoDto> updateValidator,
		IUserContext userContext
		)
	{
		_ContratoRepository = contratoRepository;
		_FornecedorRepository = fornecedorRepository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<Result<ContratoDto>> GetByIdAsync(Guid id)
	{
		var contrato = await _ContratoRepository.GetByIdWithFornecedorAsync(id);
		if (contrato == null)
		{
			return Result<ContratoDto>.Failure(new[] { "Fornecedor não encontrado." });
		}

		var dto = MapToDTO(contrato);
		return Result<ContratoDto>.Success(dto);
	}

	public async Task<Result<IEnumerable<ContratoDto>>> GetAllContratoAsync()
	{
		var contratos = await _ContratoRepository.GetAllContratoAsync();

		var dtos = contratos.Select(a => MapToDTO(a));

		return Result<IEnumerable<ContratoDto>>.Success(dtos);
	}

	public async Task<Result<ContratoDto>> GetContratoByIdAsync(Guid id)
	{
		var contrato = await _ContratoRepository.GetByIdWithFornecedorAsync(id);
		if (contrato == null)
		{
			return Result<ContratoDto>.Failure(new List<string> { "Contrato não encontrado." });
		}

		return Result<ContratoDto>.Success(MapToDTO(contrato));
	}
	public async Task<Result<Guid>> CreateContratoAsync(CreateContratoDto dto)
	{
		// // // A validação já será feita automaticamente pelo FluentValidation
		// // // Este código é apenas para demonstração de como você poderia fazer validação manual
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
		contrato.Fornecedor ??= new List<Fornecedor>();

		if (dto.FornecedorIds != null)
		{
			foreach (var fornId in dto.FornecedorIds)
			{
				var fornecedor = await _FornecedorRepository.GetByIdAsync(fornId);
				if (fornecedor != null)
				{
					contrato.Fornecedor.Add(fornecedor);
				}
			}
		}

		await _ContratoRepository.AddAsync(contrato);
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

		var contrato = await _ContratoRepository.GetByIdAsync(dto.Id);
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

		// Atualizar contratos (exemplo simples: substitui todas)
		if (dto.FornecedorIds != null)
		{
			contrato.Fornecedor.Clear();
			foreach (var fornId in dto.FornecedorIds)
			{
				var fornecedor = await _FornecedorRepository.GetByIdAsync(fornId);
				if (fornecedor != null)
				{
					contrato.Fornecedor.Add(fornecedor);
				}
			}
		}

		await _ContratoRepository.UpdateAsync(contrato);
		var resultDto = MapToDTO(contrato);
		return Result<ContratoDto>.Success(resultDto);
		
	}

	public async Task<Result> DeleteContratoAsync(Guid id)
	{
		var contrato = await _ContratoRepository.GetByIdAsync(id);
		if (contrato == null)
		{
			return Result.Failure(new List<string> { "Contrato não encontrada." });
		}


		await _ContratoRepository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> AddFornecedorAsync(Guid contratoId, Guid fornecedorId)
	{
		var contrato = await _ContratoRepository.GetByIdAsync(contratoId);
		var fornecedor = await _FornecedorRepository.GetByIdAsync(fornecedorId);

		if (contrato == null) return Result.Failure(new[] { "Contrato não encontrado." });
		if (fornecedor == null) return Result.Failure(new[] { "Fornecedor não encontrado." });

		await _ContratoRepository.AddFornecedorAsync(fornecedorId, contratoId);
		return Result.Success();
	}

	public async Task<Result> RemoveFornecedorAsync(Guid contratoId, Guid fornecedorId)
	{
		// Validações podem ser feitas aqui ou no repositório
		await _ContratoRepository.RemoveFornecedorAsync(contratoId, fornecedorId);
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
			Gerencia = contrato.Gerencia,
			Fornecedores = contrato.Fornecedor? // Mapeia contratos para serem carregados
				.Select(f => new FornecedorDto { Id = f.Id, Nome = f.Nome, CPFCNPJ = f.CPFCNPJ })
				.ToList()

		};
	}

}
