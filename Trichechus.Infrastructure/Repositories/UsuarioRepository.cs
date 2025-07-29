using Microsoft.EntityFrameworkCore;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
	private readonly TrichechusDbContext _context;

	public UsuarioRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<Usuario> GetByIdAsync(Guid id)
	{
		return await _context.Usuario
			.Include(u => u.Perfil) // Inclui perfis associados
				.ThenInclude(p => p.Funcionalidade) // Inclui funcionalidades dentro dos perfis
			.FirstOrDefaultAsync(u => u.Id == id);
	}

	public async Task<Usuario> GetByIdWithPerfilAsync(Guid id)
	{
		return await _context.Usuario
			.Include(u => u.Perfil) // Inclui perfis associados
									//.ThenInclude(p => p.Funcionalidades) // Inclui funcionalidades dentro dos perfis
			.FirstOrDefaultAsync(u => u.Id == id);
	}
	public async Task<IEnumerable<Usuario>> GetAllAsync()
	{
		return await _context.Usuario.ToListAsync();
	}

	public async Task<Usuario> GetByEmailAsync(string email)
	{
		return await _context.Usuario
			.Include(u => u.Perfil)
				.ThenInclude(p => p.Funcionalidade)
			.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
	}

	public async Task<bool> EmailExistsAsync(string email)
	{
		return await _context.Usuario.AnyAsync(u => u.Email.ToLower() == email.ToLower());
	}

	public async Task AddAsync(Usuario usuario)
	{
		await _context.Usuario.AddAsync(usuario);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Usuario usuario)
	{
		_context.Usuario.Update(usuario);
		await _context.SaveChangesAsync();
	}

	public async Task AddPerfilAsync(Guid usuarioId, Guid perfilId)
	{
		var usuario = await _context.Usuario.Include(p => p.Perfil).FirstOrDefaultAsync(p => p.Id == usuarioId);
		var perfil = await _context.Perfil.FindAsync(perfilId);

		if (usuario != null && perfil != null && !usuario.Perfil.Any(p => p.Id == perfilId))
		{
			usuario.Perfil.Add(perfil);
			await _context.SaveChangesAsync();
		}
	}

	public async Task RemovePerfilAsync(Guid usuarioId, Guid perfilId)
	{
		var usuario = await _context.Usuario.Include(p => p.Perfil).FirstOrDefaultAsync(p => p.Id == usuarioId);
		var perfil = await _context.Perfil.FindAsync(perfilId);

		if (usuario != null && perfil != null)
		{
			usuario.Perfil.Remove(perfil);
			await _context.SaveChangesAsync();
		}
	}
}