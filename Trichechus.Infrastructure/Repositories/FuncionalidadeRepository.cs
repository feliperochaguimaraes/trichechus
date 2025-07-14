using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class FuncionalidadeRepository : IFuncionalidadeRepository
{
	private readonly TrichechusDbContext _context;

	public FuncionalidadeRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Funcionalidade> GetByIdAsync(Guid id)
	{
		return await _context.Funcionalidades.FindAsync(id);
	}

	public async Task<Funcionalidade> GetByNameAsync(string nome)
	{
		return await _context.Funcionalidades.FirstOrDefaultAsync(f => f.Nome.ToLower() == nome.ToLower());
	}

	public async Task<IEnumerable<Funcionalidade>> GetAllAsync()
	{
		return await _context.Funcionalidades.ToListAsync();
	}

	public async Task AddAsync(Funcionalidade funcionalidade)
	{
		await _context.Funcionalidades.AddAsync(funcionalidade);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Funcionalidade funcionalidade)
	{
		_context.Funcionalidades.Update(funcionalidade);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var funcionalidade = await _context.Funcionalidades.FindAsync(id);
		if (funcionalidade != null)
		{
			_context.Funcionalidades.Remove(funcionalidade);
			await _context.SaveChangesAsync();
		}
	}
}