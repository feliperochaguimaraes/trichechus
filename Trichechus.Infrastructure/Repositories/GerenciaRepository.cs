using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class GerenciaRepository : IGerenciaRepository
{
	private readonly TrichechusDbContext _context;

	public GerenciaRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Gerencia> GetByIdAsync(Guid id)
	{
		// return await _context.Gerencias.FindAsync(id);
		return await _context.Gerencia
			//.Include(a => a.Tarefas.Where(t => t.DeletadoEm == null)).;
			.FindAsync(id);
	}

	public async Task<IEnumerable<Gerencia>> GetAllAsync()
	{
		return await _context.Gerencia.ToListAsync();
	}

	public async Task AddAsync(Gerencia gerencia)
	{
		await _context.Gerencia.AddAsync(gerencia);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Gerencia gerencia)
	{
		_context.Gerencia.Update(gerencia);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var gerencia = await _context.Gerencia.FindAsync(id);
		if (gerencia != null)
		{
			_context.Gerencia.Remove(gerencia);
			await _context.SaveChangesAsync();
		}
	}
}