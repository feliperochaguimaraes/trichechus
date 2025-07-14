using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class RepositorioRepository : IRepositorioRepository
{
	private readonly TrichechusDbContext _context;

	public RepositorioRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Repositorio> GetByIdAsync(Guid id)
	{
		return await _context.Repositorio.FindAsync(id);
	}
	public async Task<IEnumerable<Repositorio>> GetAllAsync()
	{
		return await _context.Repositorio.ToListAsync();
	}

	public async Task AddAsync(Repositorio tarefa)
	{
		await _context.Repositorio.AddAsync(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Repositorio tarefa)
	{
		_context.Repositorio.Update(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var tarefa = await _context.Repositorio.FindAsync(id);
		if (tarefa != null)
		{
			_context.Repositorio.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
}