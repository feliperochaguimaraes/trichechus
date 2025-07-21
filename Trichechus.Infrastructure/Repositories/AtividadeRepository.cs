using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class AtividadeRepository : IAtividadeRepository
{
	private readonly TrichechusDbContext _context;

	public AtividadeRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Atividade> GetByIdAsync(Guid id)
	{
		return await _context.Atividades.FindAsync(id);
	}

	public async Task<IEnumerable<Atividade>> GetAllAsync()
	{
		return await _context.Atividades
			.Include(a => a.Tarefas.Where(t => t.DeletadoEm == null))
			.Where(a => a.DeletadoEm == null)
			.ToListAsync();
	}

	public async Task AddAsync(Atividade atividade)
	{
		await _context.Atividades.AddAsync(atividade);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Atividade atividade)
	{
		_context.Atividades.Update(atividade);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var atividade = await _context.Atividades.FindAsync(id);
		if (atividade != null)
		{
			_context.Atividades.Remove(atividade);
			await _context.SaveChangesAsync();
		}
	}

	public async Task DeleteSoftAsync(Guid id)
	{
		var atividade = await _context.Atividades.FindAsync(id);
		if (atividade != null)
		{
			_context.Atividades.Update(atividade);
			await _context.SaveChangesAsync();
		}
	}
}