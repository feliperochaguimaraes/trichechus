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
	public async Task<URL> GetByIdWithSoftwareAsync(Guid id)
	{
		return await _context.URL
		   .Include(p => p.Software)
		   .FirstOrDefaultAsync(p => p.Id == id);
	}
	public async Task<IEnumerable<URL>> GetAllUrlAsync()
	{
		return await _context.URL
			.Include(a => a.Software)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

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
	
	public async Task AddSoftUrlAsync(Guid urlId, Guid softwareId)
	{
		var url = await _context.Catalogo.Include(p => p.Software).FirstOrDefaultAsync(p => p.Id == softwareId);
		var software = await _context.Software.FindAsync(softwareId);

		if (url != null && software != null && !url.Software.Any(f => f.Id == softwareId))
		{
			url.Software.Add(software);
			await _context.SaveChangesAsync();
		}
	}

	public async Task DeleteSoftUrlAsync(Guid urlId, Guid softwareId)
	{
		var url = await _context.Catalogo.Include(p => p.Software).FirstOrDefaultAsync(p => p.Id == urlId);
		var software = url?.Software.FirstOrDefault(f => f.Id == softwareId);

		if (url != null && software != null)
		{
			url.Software.Remove(software);
			await _context.SaveChangesAsync();
		}
	}
    

    
}

