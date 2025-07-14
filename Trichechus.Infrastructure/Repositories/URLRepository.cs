using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class URLRepository : IURLRepository
{
	private readonly TrichechusDbContext _context;

	public URLRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<URL> GetByIdAsync(Guid id)
	{
		return await _context.URL.FindAsync(id);
	}
	public async Task<IEnumerable<URL>> GetAllAsync()
	{
		return await _context.URL.ToListAsync();
	}

	public async Task AddAsync(URL tarefa)
	{
		await _context.URL.AddAsync(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(URL tarefa)
	{
		_context.URL.Update(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var tarefa = await _context.URL.FindAsync(id);
		if (tarefa != null)
		{
			_context.URL.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
}