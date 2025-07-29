using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
	private readonly TrichechusDbContext _context;

	public TarefaRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Tarefa> GetByIdAsync(Guid id)
	{
		return await _context.Tarefa.FindAsync(id);
	}
	public async Task<IEnumerable<Tarefa>> GetAllAsync()
	{
		return await _context.Tarefa
			.Where(a => a.DeletadoEm == null)
			.ToListAsync();
	}

	public async Task AddAsync(Tarefa tarefa)
	{
		await _context.Tarefa.AddAsync(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Tarefa tarefa)
	{
		_context.Tarefa.Update(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var tarefa = await _context.Tarefa.FindAsync(id);
		if (tarefa != null)
		{
			_context.Tarefa.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
}