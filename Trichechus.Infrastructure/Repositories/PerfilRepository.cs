using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class PerfilRepository : IPerfilRepository
{
    private readonly TrichechusDbContext _context;

    public PerfilRepository(TrichechusDbContext context)
    {
        _context = context;
    }

    public async Task<Perfil> GetByIdAsync(Guid id)
    {
        return await _context.Perfis.FindAsync(id);
    }

    public async Task<Perfil> GetByIdWithFuncionalidadesAsync(Guid id)
    {
        return await _context.Perfis
           .Include(p => p.Funcionalidades)
           .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Perfil> GetByNameAsync(string nome)
    {
        return await _context.Perfis
            .Include(p => p.Funcionalidades)
            .FirstOrDefaultAsync(p => p.Nome.ToLower() == nome.ToLower());
    }

    public async Task<IEnumerable<Perfil>> GetAllAsync()
    {
        return await _context.Perfis.ToListAsync();
    }

    public async Task AddAsync(Perfil perfil)
    {
        await _context.Perfis.AddAsync(perfil);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Perfil perfil)
    {
        _context.Perfis.Update(perfil);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var perfil = await _context.Perfis.FindAsync(id);
        if (perfil != null)
        {
            _context.Perfis.Remove(perfil);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId)
    {
        var perfil = await _context.Perfis.Include(p => p.Funcionalidades).FirstOrDefaultAsync(p => p.Id == perfilId);
        var funcionalidade = await _context.Funcionalidades.FindAsync(funcionalidadeId);

        if (perfil != null && funcionalidade != null && !perfil.Funcionalidades.Any(f => f.Id == funcionalidadeId))
        {
            perfil.Funcionalidades.Add(funcionalidade);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveFuncionalidadeAsync(Guid perfilId, Guid funcionalidadeId)
    {
        var perfil = await _context.Perfis.Include(p => p.Funcionalidades).FirstOrDefaultAsync(p => p.Id == perfilId);
        var funcionalidade = perfil?.Funcionalidades.FirstOrDefault(f => f.Id == funcionalidadeId);

        if (perfil != null && funcionalidade != null)
        {
            perfil.Funcionalidades.Remove(funcionalidade);
            await _context.SaveChangesAsync();
        }
    }
}