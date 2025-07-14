using FluentValidation;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Application.Interfaces;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Application.Services
{
	public class TarefaService : ITarefaService
	{
		private readonly ITarefaRepository _repository;
		private readonly IAtividadeRepository _atividadeRepository;
		private readonly IValidator<CreateTarefaDto> _createValidator;
		private readonly IValidator<UpdateTarefaDto> _updateValidator;

		public TarefaService(
					ITarefaRepository repository,
					IAtividadeRepository atividadeRepository,
					IValidator<CreateTarefaDto> createValidator,
					IValidator<UpdateTarefaDto> updateValidator)
		{
			_repository = repository;
			_atividadeRepository = atividadeRepository;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
		}

		public async Task<IEnumerable<TarefaDto>> GetAllTarefasAsync()
		{
			var tarefas = await _repository.GetAllAsync();
			return tarefas.Select(t => MapToDTO(t));
		}

		public async Task<Result<TarefaDto>> GetTarefaByIdAsync(Guid id)
		{
			var tarefa = await _repository.GetByIdAsync(id);
			if (tarefa == null)
			{
				return Result<TarefaDto>.Failure(new List<string> { "Tarefa não encontrada." });
			}

			return Result<TarefaDto>.Success(MapToDTO(tarefa));
		}

		public async Task<IEnumerable<TarefaDto>> GetTarefasByAtividadeIdAsync(Guid atividadeId)
		{
			var atividade = await _atividadeRepository.GetByIdAsync(atividadeId);
			if (atividade == null || atividade.Tarefas == null)
			{
				return Enumerable.Empty<TarefaDto>();
			}

			return atividade.Tarefas.Select(t => MapToDTO(t));
		}

		public async Task<Result<Guid>> CreateTarefaAsync(CreateTarefaDto dto)
		{
			// A validação já será feita automaticamente pelo FluentValidation
			// Este código é apenas para demonstração de como você poderia fazer validação manual
			var validationResult = await _createValidator.ValidateAsync(dto);
			if (!validationResult.IsValid)
			{
				return Result<Guid>.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
			}

			// Verificar se a atividade existe
			var atividade = await _atividadeRepository.GetByIdAsync(dto.AtividadeId);
			if (atividade == null)
			{
				return Result<Guid>.Failure(new List<string> { "Atividade não encontrada." });
			}

			var tarefa = new Tarefa
			{
				Titulo = dto.Titulo,
				Descricao = dto.Descricao,
				Prazo = dto.Prazo,
				NomeResponsavel = dto.NomeResponsavel,
				Situacao = dto.Situacao,
				Observacao = dto.Observacao,
				AtividadeId = dto.AtividadeId,
				CriadoEm = DateTime.Now
			};

			await _repository.AddAsync(tarefa);
			return Result<Guid>.Success(tarefa.Id);
		}

		public async Task<Result> UpdateTarefaAsync(UpdateTarefaDto dto)
		{
			// A validação já será feita automaticamente pelo FluentValidation
			// Este código é apenas para demonstração de como você poderia fazer validação manual
			var validationResult = await _updateValidator.ValidateAsync(dto);
			if (!validationResult.IsValid)
			{
				return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
			}

			var tarefa = await _repository.GetByIdAsync(dto.Id);
			if (tarefa == null)
			{
				return Result.Failure(new List<string> { "Tarefa não encontrada." });
			}

			// Verificar se a atividade existe
			if (tarefa.AtividadeId != dto.AtividadeId)
			{
				var atividade = await _atividadeRepository.GetByIdAsync(dto.AtividadeId);
				if (atividade == null)
				{
					return Result.Failure(new List<string> { "Atividade não encontrada." });
				}
			}

			tarefa.Titulo = dto.Titulo;
			tarefa.Descricao = dto.Descricao;
			tarefa.Prazo = dto.Prazo;
			tarefa.NomeResponsavel = dto.NomeResponsavel;
			tarefa.Situacao = dto.Situacao;
			tarefa.Observacao = dto.Observacao;
			tarefa.AtividadeId = dto.AtividadeId;
			tarefa.AtualizadoEm = DateTime.Now;

			await _repository.UpdateAsync(tarefa);
			return Result.Success();
		}

		public async Task<Result> DeleteTarefaAsync(Guid id)
		{
			var tarefa = await _repository.GetByIdAsync(id);
			if (tarefa == null)
			{
				return Result.Failure(new List<string> { "Tarefa não encontrada." });
			}

			// Soft delete
			tarefa.DeletadoEm = DateTime.Now;
			await _repository.UpdateAsync(tarefa);

			// Ou hard delete
			// await _repository.DeleteAsync(id);

			return Result.Success();
		}

		private TarefaDto MapToDTO(Tarefa tarefa)
		{
			return new TarefaDto
			{
				Id = tarefa.Id,
				Titulo = tarefa.Titulo,
				Descricao = tarefa.Descricao,
				Prazo = tarefa.Prazo,
				NomeResponsavel = tarefa.NomeResponsavel,
				Situacao = tarefa.Situacao,
				Observacao = tarefa.Observacao,
				CriadoEm = tarefa.CriadoEm,
				AtualizadoEm = tarefa.AtualizadoEm,
				AtividadeId = tarefa.AtividadeId
			};
		}
	}
}
