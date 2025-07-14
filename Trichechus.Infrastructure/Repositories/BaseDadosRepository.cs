using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class BaseDadosRepository : IBaseDadosRepository
{
	private readonly TrichechusDbContext _context;

	public BaseDadosRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<BaseDados> GetByIdAsync(Guid id)
	{
		return await _context.BaseDados.FindAsync(id);
	}
	public async Task<IEnumerable<BaseDados>> GetAllAsync()
	{
		return await _context.BaseDados.ToListAsync();
	}

	public async Task AddAsync(BaseDados tarefa)
	{
		await _context.BaseDados.AddAsync(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(BaseDados tarefa)
	{
		_context.BaseDados.Update(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var tarefa = await _context.BaseDados.FindAsync(id);
		if (tarefa != null)
		{
			_context.BaseDados.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
}