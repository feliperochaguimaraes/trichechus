using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class ContratoRepository : IContratoRepository
{
	private readonly TrichechusDbContext _context;

	public ContratoRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Contrato> GetByIdAsync(Guid id)
	{
		return await _context.Contratos.FindAsync(id);
	}

	public async Task<IEnumerable<Contrato>> GetAllAsync()
	{
		return await _context.Contratos
			// .Include(a => a.Tarefas)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

	}

	public async Task AddAsync(Contrato fornecedor)
	{
		await _context.Contratos.AddAsync(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Contrato fornecedor)
	{
		_context.Contratos.Update(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var fornecedor = await _context.Contratos.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Contratos.Remove(fornecedor);
			await _context.SaveChangesAsync();
		}
	}

	public async Task DeleteSoftAsync(Guid id)
	{
		var fornecedor = await _context.Contratos.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Contratos.Update(fornecedor);
			await _context.SaveChangesAsync();
		}
	}
}