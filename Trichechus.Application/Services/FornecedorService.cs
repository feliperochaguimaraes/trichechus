using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;


namespace Trichechus.Application.Services;

public class FornecedorService : IFornecedorService
{
	private readonly IFornecedorRepository _repository;
	private readonly IValidator<CreateFornecedorDto> _createValidator;
	private readonly IValidator<UpdateFornecedorDto> _updateValidator;
	private readonly IUserContext _userContext;
	private readonly IFornecedorRepository _fornecedorRepository;
	private readonly IContratoRepository _contratoRepository;

	public FornecedorService(
		IFornecedorRepository repository,
		IValidator<CreateFornecedorDto> createValidator,
		IValidator<UpdateFornecedorDto> updateValidator,
		IUserContext userContext,
		IFornecedorRepository fornecedorRepository,
		IContratoRepository contratoRepository
		)
	{
		_fornecedorRepository = fornecedorRepository;
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
		_contratoRepository = contratoRepository;
	}
	public async Task<IEnumerable<FornecedorDto>> GetAllFornecedorAsync()
	{
		var fornecedor = await _repository.GetAllAsync();
		return fornecedor.Select(a => MapToDTO(a));
	}

	public async Task<Result<FornecedorDto>> GetFornecedorByIdAsync(Guid id)
	{
		var fornecedor = await _repository.GetByIdAsync(id);
		if (fornecedor == null)
		{
			return Result<FornecedorDto>.Failure(new List<string> { "Fornecedor não encontrada." });
		}

		return Result<FornecedorDto>.Success(MapToDTO(fornecedor));
	}
	public async Task<Result<Guid>> CreateFornecedorAsync(CreateFornecedorDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var fornecedor = new Fornecedor
		{
			Nome = dto.Nome,
			CPFCNPJ = dto.CPFCNPJ,
			Endereco = dto.Endereco,
			Numero = dto.Numero,
			Cep = dto.Cep,
			Cidade = dto.Cidade,
			Estado = dto.Estado,
			Ativo = dto.Ativo
		};

		await _repository.AddAsync(fornecedor);
		return Result<Guid>.Success(fornecedor.Id);
	}

	public async Task<Result> UpdateFornecedorAsync(UpdateFornecedorDto dto)
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
			return Result.Failure(new List<string> { "Fornecedor não encontrada." });
		}

		fornecedor.Nome = dto.Nome;
		fornecedor.CPFCNPJ = dto.CPFCNPJ;
		fornecedor.Endereco = dto.Endereco;
		fornecedor.Numero = dto.Numero;
		fornecedor.Cep = dto.Cep;
		fornecedor.Cidade = dto.Cidade;
		fornecedor.Estado = dto.Estado;
		fornecedor.Ativo = dto.Ativo;

		await _repository.UpdateAsync(fornecedor);
		return Result.Success();
	}

	public async Task<Result> DeleteFornecedorAsync(Guid id)
	{
		var fornecedor = await _repository.GetByIdAsync(id);
		if (fornecedor == null)
		{
			return Result.Failure(new List<string> { "Fornecedor não encontrada." });
		}

		// Soft delete
		// fornecedor.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(fornecedor);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> AddContratoAsync(Guid fornecedorId, Guid contratoId)
	{
		var fornecedor = await _fornecedorRepository.GetByIdAsync(fornecedorId);
		var contrato = await _contratoRepository.GetByIdAsync(contratoId);

		if (fornecedor == null) return Result.Failure(new[] { "Fornecedor não encontrado." });
		if (contrato == null) return Result.Failure(new[] { "Contrato não encontrado." });

		await _fornecedorRepository.AddContratoAsync(fornecedorId, contratoId);
		return Result.Success();
	}

	public async Task<Result> RemoveContratoAsync(Guid fornecedorId, Guid contratoId)
	{
		// Validações podem ser feitas aqui ou no repositório
		await _fornecedorRepository.RemoveContratoAsync(fornecedorId, contratoId);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftFornecedorAsync(Guid id)
	{
		var fornecedor = await _repository.GetByIdAsync(id);
		if (fornecedor == null)
		{
			return Result.Failure(new List<string> { "Fornecedor não encontrada." });
		}

		await _repository.UpdateAsync(fornecedor);
		return Result.Success();
	}

	private FornecedorDto MapToDTO(Fornecedor fornecedor)
	{
		return new FornecedorDto
		{
			Id = fornecedor.Id,
			Nome = fornecedor.Nome,
			CPFCNPJ = fornecedor.CPFCNPJ,
			Endereco = fornecedor.Endereco,
			Numero = fornecedor.Numero,
			Cep = fornecedor.Cep,
			Cidade = fornecedor.Cidade,
			Estado = fornecedor.Estado,
			Ativo = fornecedor.Ativo,
			Contratos = fornecedor.Contrato? // Mapeia contratos para serem carregados
					.Select(f => new ContratoDto { Id = f.Id, NomeAlias = f.NomeAlias, Numero = f.Numero })
					.ToList()
		};
	}
}
	




