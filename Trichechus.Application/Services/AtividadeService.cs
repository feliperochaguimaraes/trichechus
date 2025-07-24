using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services;

public class AtividadeService : IAtividadeService
{
	private readonly IAtividadeRepository _repository;
	private readonly IValidator<CreateAtividadeDto> _createValidator;
	private readonly IValidator<UpdateAtividadeDto> _updateValidator;
	private readonly IUserContext _userContext;

	public AtividadeService(
		IAtividadeRepository repository,
		IValidator<CreateAtividadeDto> createValidator,
		IValidator<UpdateAtividadeDto> updateValidator,
		IUserContext userContext
		)
	{
		_repository = repository;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
		_userContext = userContext;
	}
	public async Task<IEnumerable<AtividadeDto>> GetAllAtividadesAsync()
	{
		var atividades = await _repository.GetAllAsync();
		return atividades.Select(a => MapToDto(a));
	}

	public async Task<Result<AtividadeDto>> GetAtividadeByIdAsync(Guid id)
	{
		var atividade = await _repository.GetByIdWithTarefasAsync(id);
		if (atividade == null)
		{
			return Result<AtividadeDto>.Failure(new List<string> { "Atividade não encontrada." });
		}

		var dto = MapToDto(atividade);
		return Result<AtividadeDto>.Success(dto);
	}
	public async Task<Result<Guid>> CreateAtividadeAsync(CreateAtividadeDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _createValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var atividade = new Atividade
		{
			Titulo = dto.Titulo,
			Descricao = dto.Descricao,
			Situacao = dto.Situacao,
			NomeResponsavel = dto.NomeResponsavel,
			Prazo = dto.Prazo,
			NomeEquipeResponsavel = dto.NomeEquipeResponsavel,
			TipoEntrada = dto.TipoEntrada,
			CriadoEm = DateTime.Now
		};

		await _repository.AddAsync(atividade);
		return Result<Guid>.Success(atividade.Id);
	}

	public async Task<Result> UpdateAtividadeAsync(UpdateAtividadeDto dto)
	{
		// A validação já será feita automaticamente pelo FluentValidation
		// Este código é apenas para demonstração de como você poderia fazer validação manual
		var validationResult = await _updateValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
		}

		var atividade = await _repository.GetByIdAsync(dto.Id);
		if (atividade == null)
		{
			return Result.Failure(new List<string> { "Atividade não encontrada." });
		}

		atividade.Titulo = dto.Titulo;
		atividade.Descricao = dto.Descricao;
		atividade.Situacao = dto.Situacao;
		atividade.NomeResponsavel = dto.NomeResponsavel;
		atividade.Prazo = dto.Prazo;
		atividade.NomeEquipeResponsavel = dto.NomeEquipeResponsavel;
		atividade.TipoEntrada = dto.TipoEntrada;
		atividade.AtualizadoEm = DateTime.Now;

		await _repository.UpdateAsync(atividade);
		return Result.Success();
	}

	public async Task<Result> DeleteAtividadeAsync(Guid id)
	{
		var atividade = await _repository.GetByIdAsync(id);
		if (atividade == null)
		{
			return Result.Failure(new List<string> { "Atividade não encontrada." });
		}

		// Soft delete
		// atividade.DeletadoEm = DateTime.Now;
		// await _repository.UpdateAsync(atividade);

		// Ou hard delete
		await _repository.DeleteAsync(id);
		return Result.Success();
	}

	public async Task<Result> DeleteSoftAtividadeAsync(Guid id)
	{
		var atividade = await _repository.GetByIdAsync(id);
		if (atividade == null)
		{
			return Result.Failure(new List<string> { "Atividade não encontrada." });
		}
		atividade.DeletadoEm = DateTime.Now;
		
		await _repository.UpdateAsync(atividade);
		return Result.Success();
	}

	private AtividadeDto MapToDto(Atividade atividade)
	{
		return new AtividadeDto
		{
			Id = atividade.Id,
			Titulo = atividade.Titulo,
			Descricao = atividade.Descricao,
			Situacao = atividade.Situacao,
			NomeResponsavel = atividade.NomeResponsavel,
			Prazo = atividade.Prazo,
			NomeEquipeResponsavel = atividade.NomeEquipeResponsavel,
			CriadoEm = atividade.CriadoEm,
			AtualizadoEm = atividade.AtualizadoEm,
			TipoEntrada = atividade.TipoEntrada,
			Tarefas = atividade.Tarefas?.Select(t => new TarefaDto
			{
				Id = t.Id,
				Titulo = t.Titulo,
				Descricao = t.Descricao,
				Prazo = t.Prazo,
				NomeResponsavel = t.NomeResponsavel,
				Situacao = t.Situacao,
				Observacao = t.Observacao,
				CriadoEm = t.CriadoEm,
				AtualizadoEm = t.AtualizadoEm,
				AtividadeId = t.AtividadeId
			}).ToList()
		};
	}
}
