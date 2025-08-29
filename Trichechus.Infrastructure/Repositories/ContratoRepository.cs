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
		return await _context.Contrato.FindAsync(id);
	}

	public async Task<Contrato> GetByIdWithFornecedorAsync(Guid id)
    {
        return await _context.Contrato
           .Include(p => p.Fornecedor)
           .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Contrato> GetByNameAsync(string Nome)
    {
        return await _context.Contrato
            .Include(p => p.Fornecedor)
            .FirstOrDefaultAsync(p => p.NomeAlias.ToLower() == Nome.ToLower());
    } 
	public async Task<IEnumerable<Contrato>> GetAllContratoAsync()
    {
        return await _context.Contrato
			.Include(a => a.Fornecedor)
			// .Where(a => a.DeletadoEm == null)
			.ToListAsync();

    }


	public async Task AddAsync(Contrato fornecedor)
	{
		await _context.Contrato.AddAsync(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Contrato fornecedor)
	{
		_context.Contrato.Update(fornecedor);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var fornecedor = await _context.Contrato.FindAsync(id);
		if (fornecedor != null)
		{
			_context.Contrato.Remove(fornecedor);
			await _context.SaveChangesAsync();
		}
	}


public async Task AddFornecedorAsync(Guid contratoId, Guid fornecedorId)
    {
        var contrato = await _context.Contrato.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == fornecedorId);
        var fornecedor = await _context.Fornecedor.FindAsync(fornecedorId);

        if (contrato != null && fornecedor != null && !contrato.Fornecedor.Any(f => f.Id == fornecedorId))
        {
            contrato.Fornecedor.Add(fornecedor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFornecedorAsync(Guid contratoId, Guid fornecedorId)
    {
        var contrato = await _context.Contrato.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == contratoId);
        var fornecedor = contrato?.Fornecedor.FirstOrDefault(f => f.Id == fornecedorId);

        if (contrato != null && fornecedor != null)
        {
            contrato.Fornecedor.Remove(fornecedor);
            await _context.SaveChangesAsync();
        }
    }
    
}