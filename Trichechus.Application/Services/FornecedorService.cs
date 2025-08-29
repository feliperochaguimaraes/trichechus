using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;



namespace Trichechus.Application.Services;

public class FornecedorService : IFornecedorService
{
	private readonly IFornecedorRepository _FornecedorRepository;
	private readonly IContratoRepository _contratoRepository;
	private readonly IValidator<CreateFornecedorDto> _createValidator;
	private readonly IValidator<UpdateFornecedorDto> _updateValidator;
	private readonly IUserContext _userContext;


	public FornecedorService(
		IValidator<CreateFornecedorDto> createValidator,
		IValidator<UpdateFornecedorDto> updateValidator,
		IUserContext userContext,
		IFornecedorRepository fornecedorRepository,
		IContratoRepository contratoRepository
		)
	{

		_FornecedorRepository = fornecedorRepository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
		_contratoRepository = contratoRepository;
	}

	public async Task<Result<FornecedorDto>> GetByIdAsync(Guid id)
		{
			var fornecedor = await _FornecedorRepository.GetByIdWithContratosAsync(id);
			if (fornecedor == null)
			{
				return Result<FornecedorDto>.Failure(new[] { "Fornecedor não encontrado." });
			}

			var dto = MapToDTO(fornecedor);
			return Result<FornecedorDto>.Success(dto);
		}

	public async Task<Result<IEnumerable<FornecedorDto>>> GetAllAsync()
	{
		var fornecedores = await _FornecedorRepository.GetAllAsync();


		var dtos = fornecedores.Select(a => MapToDTO(a));

		return Result<IEnumerable<FornecedorDto>>.Success(dtos);
	}

	public async Task<Result<FornecedorDto>> GetFornecedorByIdAsync(Guid id)
	{
		var fornecedor = await _FornecedorRepository.GetByIdWithContratosAsync(id);
		if (fornecedor == null)
		{
			return Result<FornecedorDto>.Failure(new List<string> { "Contrato não encontrado." });
		}
		return Result<FornecedorDto>.Success(MapToDTO(fornecedor));
	}


	public async Task<Result<Guid>> CreateFornecedorAsync(CreateFornecedorDto dto)
	{
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());


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

		fornecedor.Contrato ??= new List<Contrato>();


		if (dto.ContratoIds != null)
		{
			foreach (var contId in dto.ContratoIds)
			{
				var contrato = await _contratoRepository.GetByIdAsync(contId);
				if (contrato != null)
				{
					fornecedor.Contrato.Add(contrato);
				}

			}
		}
		await _FornecedorRepository.AddAsync(fornecedor);
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

		var fornecedor = await _FornecedorRepository.GetByIdAsync(dto.Id);
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

		// Atualizar contratos (exemplo simples: substitui todas)
		if (dto.ContratoIds != null)
		{
			fornecedor.Contrato.Clear();
			foreach (var contId in dto.ContratoIds)
			{
				var contrato = await _contratoRepository.GetByIdAsync(contId);
				if (contrato != null)
				{
					fornecedor.Contrato.Add(contrato);
				}
			}
		}

		await _FornecedorRepository.UpdateAsync(fornecedor);
		var resultDto = MapToDTO(fornecedor);
		return Result<FornecedorDto>.Success(resultDto);

	}

	public async Task<Result> DeleteFornecedorAsync(Guid id)
	{
		var fornecedor = await _FornecedorRepository.GetByIdAsync(id);
		if (fornecedor == null) return Result.Failure(new[] { "Fornecedor não encontrado." });

		// Adicionar validação: verificar se o perfil está em uso por algum usuário antes de excluir?

		await _FornecedorRepository.DeleteAsync(id);
		return Result.Success();

	}

	public async Task<Result> AddContratoAsync(Guid fornecedorId, Guid contratoId)
	{
		var fornecedor = await _FornecedorRepository.GetByIdAsync(fornecedorId);
		var contrato = await _contratoRepository.GetByIdAsync(contratoId);

		if (fornecedor == null) return Result.Failure(new[] { "Fornecedor não encontrado." });
		if (contrato == null) return Result.Failure(new[] { "Contrato não encontrado." });

		await _FornecedorRepository.AddContratoAsync(fornecedorId, contratoId);
		return Result.Success();
	}

	public async Task<Result> RemoveContratoAsync(Guid fornecedorId, Guid contratoId)
	{
		// Validações podem ser feitas aqui ou no repositório
		await _FornecedorRepository.RemoveContratoAsync(fornecedorId, contratoId);
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





