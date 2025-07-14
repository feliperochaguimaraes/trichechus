using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class SoftwareRepository : ISoftwareRepository
{
	private readonly TrichechusDbContext _context;

	public SoftwareRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Software> GetByIdAsync(Guid id)
	{
		return await _context.Software.FindAsync(id);
	}
	public async Task<IEnumerable<Software>> GetAllAsync()
	{
		return await _context.Software.ToListAsync();
	}

	public async Task AddAsync(Software tarefa)
	{
		await _context.Software.AddAsync(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Software tarefa)
	{
		_context.Software.Update(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var tarefa = await _context.Software.FindAsync(id);
		if (tarefa != null)
		{
			_context.Software.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
}