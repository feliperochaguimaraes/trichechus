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
	public async Task<BaseDados> GetByIdWithSoftwareAsync(Guid id)
	{
		return await _context.BaseDados
		   .Include(p => p.Software)
		   .FirstOrDefaultAsync(p => p.Id == id);
	}
	public async Task<BaseDados> GetByNameAsync(string Nome)
	{
		return await _context.BaseDados
			.Include(p => p.Software)
			.FirstOrDefaultAsync(p => p.Cluster.ToLower() == Nome.ToLower());
	}
	public async Task<IEnumerable<BaseDados>> GetAllAsync()
	{
		return await _context.BaseDados.ToListAsync();
	}
	public async Task<IEnumerable<BaseDados>> GetAllBaseDadosAsync()
	{
		return await _context.BaseDados
			.Include(a => a.Software)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

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
	
	public async Task AddSoftAsync(Guid baseDadosId, Guid softwareId)
	{
		var baseDados = await _context.BaseDados.Include(p => p.Software).FirstOrDefaultAsync(p => p.Id == softwareId);
		var software = await _context.Software.FindAsync(softwareId);

		if (baseDados != null && software != null && !baseDados.Software.Any(f => f.Id == softwareId))
		{
			baseDados.Software.Add(software);
			await _context.SaveChangesAsync();
		}
	}

    public async Task RemoveSoftBaseDadosAsync(Guid baseDadosId, Guid softwareId)
    {
        var baseDados = await _context.Contrato.Include(p => p.Software).FirstOrDefaultAsync(p => p.Id == baseDadosId);
        var software = baseDados?.Software.FirstOrDefault(f => f.Id == softwareId);

        if (baseDados != null && software != null)
        {
            baseDados.Software.Remove(software);
            await _context.SaveChangesAsync();
        }
    }
}