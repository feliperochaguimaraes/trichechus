using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class CatalogoRepository : ICatalogoRepository
{
	private readonly TrichechusDbContext _context;

	public CatalogoRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Catalogo> GetByIdAsync(Guid id)
	{
		return await _context.Catalogo.FindAsync(id);
	}
	public async Task<Catalogo> GetByIdWithSoftwareAsync(Guid id)
	{
		return await _context.Catalogo
		   .Include(p => p.Software)
		   .FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<IEnumerable<Catalogo>> GetAllAsync()
	{
		return await _context.Catalogo.ToListAsync();
	}
	public async Task<IEnumerable<Catalogo>> GetAllCatalogoAsync()
	{
		return await _context.Catalogo
			.Include(a => a.Software)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

	}
	public async Task AddAsync(Catalogo tarefa)
	{
		await _context.Catalogo.AddAsync(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Catalogo tarefa)
	{
		_context.Catalogo.Update(tarefa);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var tarefa = await _context.Catalogo.FindAsync(id);
		if (tarefa != null)
		{
			_context.Catalogo.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
	
}