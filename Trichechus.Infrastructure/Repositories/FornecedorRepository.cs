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
		return await _context.Fornecedor.FindAsync(id);
	}

	public async Task<IEnumerable<Fornecedor>> GetAllAsync()
	{
		return await _context.Fornecedor
			// .Include(a => a.Tarefas)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

	}

	public async Task AddAsync(Fornecedor fornecedor)
	{
		await _context.Fornecedor.AddAsync(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Fornecedor fornecedor)
	{
		_context.Fornecedor.Update(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var fornecedor = await _context.Fornecedor.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Fornecedor.Remove(fornecedor);
			await _context.SaveChangesAsync();
		}
	}

	public async Task DeleteSoftAsync(Guid id)
	{
		var fornecedor = await _context.Fornecedor.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Fornecedor.Update(fornecedor);
			await _context.SaveChangesAsync();
		}
	}

    public async Task AddContratoAsync(Guid fornecedorId, Guid contratoId)
    {
         var fornecedor = await _context.Fornecedor.Include(p => p.Contrato).FirstOrDefaultAsync(p => p.Id == fornecedorId);
        var contrato = await _context.Contrato.FindAsync(contratoId);

        if (fornecedor != null && contrato != null && !fornecedor.Contrato.Any(f => f.Id == contratoId))
        {
            fornecedor.Contrato.Add(contrato);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveContratoAsync(Guid fornecedorId, Guid contratoId)
    {
        var fornecedor = await _context.Fornecedor.Include(p => p.Contrato).FirstOrDefaultAsync(p => p.Id == fornecedorId);
        var contrato = fornecedor?.Contrato.FirstOrDefault(f => f.Id == contratoId);

        if (fornecedor != null && contrato != null)
        {
            fornecedor.Contrato.Remove(contrato);
            await _context.SaveChangesAsync();
        }
    }

    public Task<Fornecedor> GetByIdWithContratosAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}