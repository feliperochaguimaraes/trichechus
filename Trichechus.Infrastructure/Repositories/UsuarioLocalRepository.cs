using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using Trichechus.Infrastructure.Context;

namespace Trichechus.Infrastructure.Repositories;

public class UsuarioLocalRepository : IUsuarioLocalRepository
{
	private readonly TrichechusDbContext _context;

	public UsuarioLocalRepository(TrichechusDbContext context)
	{
		_context = context;
	}

	public async Task<UsuarioLocal> GetByIdAsync(Guid id)
	{
		return await _context.UsuariosLocais
			.Include(u => u.Perfis) // Inclui perfis associados
				.ThenInclude(p => p.Funcionalidades) // Inclui funcionalidades dentro dos perfis
			.FirstOrDefaultAsync(u => u.Id == id);
	}

	public async Task<UsuarioLocal> GetByEmailAsync(string email)
	{
		return await _context.UsuariosLocais
			.Include(u => u.Perfis)
				.ThenInclude(p => p.Funcionalidades)
			.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
	}

	public async Task<bool> EmailExistsAsync(string email)
	{
		return await _context.UsuariosLocais.AnyAsync(u => u.Email.ToLower() == email.ToLower());
	}

	public async Task AddAsync(UsuarioLocal usuario)
	{
		await _context.UsuariosLocais.AddAsync(usuario);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(UsuarioLocal usuario)
	{
		_context.UsuariosLocais.Update(usuario);
		await _context.SaveChangesAsync();
	}
}