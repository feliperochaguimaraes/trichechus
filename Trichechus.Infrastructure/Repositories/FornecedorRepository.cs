using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
	private readonly TrichechusDbContext _context;

	public FornecedorRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Fornecedor> GetByIdAsync(Guid id)
	{
		return await _context.Fornecedores.FindAsync(id);
	}

	public async Task<IEnumerable<Fornecedor>> GetAllAsync()
	{
		return await _context.Fornecedores
			// .Include(a => a.Tarefas)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

	}

	public async Task AddAsync(Fornecedor fornecedor)
	{
		await _context.Fornecedores.AddAsync(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Fornecedor fornecedor)
	{
		_context.Fornecedores.Update(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var fornecedor = await _context.Fornecedores.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Fornecedores.Remove(fornecedor);
			await _context.SaveChangesAsync();
		}
	}

	public async Task DeleteSoftAsync(Guid id)
	{
		var fornecedor = await _context.Fornecedores.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Fornecedores.Update(fornecedor);
			await _context.SaveChangesAsync();
		}
	}
}