using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using BC = BCrypt.Net.BCrypt; // Alias para BCrypt

namespace Trichechus.Application.Services;

public class UsuarioService : IUsuarioService
{
	private readonly IUsuarioRepository _usuarioRepository;
	private readonly IPerfilRepository _perfilRepository;

	public UsuarioService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
	{
		_usuarioRepository = usuarioRepository;
		_perfilRepository = perfilRepository;
	}

	public async Task<Result<UsuarioDto>> GetByIdAsync(Guid id)
	{
		var usuario = await _usuarioRepository.GetByIdWithPerfilAsync(id);
		if (usuario == null)
		{
			return Result<UsuarioDto>.Failure(new[] { "Usuario não encontrado." });
		}

		var dto = MapToDto(usuario);
		return Result<UsuarioDto>.Success(dto);
	}

	public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
	{
		var usuarios = await _usuarioRepository.GetAllAsync();
		return usuarios.Select(t => MapToDto(t));

		// var usuarios = await _usuarioRepository.GetAllAsync();
		// var dto = MapToDto(usuario);
		// return Result<UsuarioDto>.Success(dto);
	}

	public async Task<Result<UsuarioDto>> CreateAsync(CreateUsuarioDto dto)
	{
		if (await _usuarioRepository.GetByEmailAsync(dto.Email) != null)
		{
			return Result<UsuarioDto>.Failure(new[] { $"Já existe o e-mail '{dto.Email}	' cadastrado." });
		}

		var usuario = new Usuario
		{
			Nome = dto.Nome,
			Email = dto.Email,
			Matricula = dto.Matricula,
			GerenciaId = dto.GerenciaId,
			SenhaHash = BC.HashPassword(dto.SenhaHash),
			Ativo = dto.Ativo,
			CriadoEm = dto.CriadoEm,
			AtualizadoEm = dto.AtualizadoEm,
			Perfil = new List<Perfil>()
		};

		// Adicionar funcionalidades se IDs foram fornecidos
		if (dto.PerfilIds != null)
		{
			foreach (var funcId in dto.PerfilIds)
			{
				var perfil = await _perfilRepository.GetByIdAsync(funcId);
				if (perfil != null)
				{
					usuario.Perfil.Add(perfil);
				}
				// Opcional: retornar erro se funcionalidade não existir
			}
		}

		await _usuarioRepository.AddAsync(usuario);
		var resultDto = MapToDto(usuario);
		return Result<UsuarioDto>.Success(resultDto);
	}

	public async Task<Result> UpdateAsync(Guid id, UpdateUsuarioDto dto)
	{
		var usuario = await _usuarioRepository.GetByIdWithPerfilAsync(id);
		if (usuario == null) return Result.Failure(new[] { "Usuario não encontrado." });

		var existenteComNome = await _usuarioRepository.GetByEmailAsync(dto.Email);
		if (existenteComNome != null && existenteComNome.Id != id)
		{
			return Result.Failure(new[] { $"Já existe o e-mail '{dto.Email}	' cadastrado." });
		}

		usuario.Nome = dto.Nome;
		usuario.Email = dto.Email;
		usuario.GerenciaId = dto.GerenciaId;
		usuario.Matricula = dto.Matricula;
		usuario.SenhaHash = BC.HashPassword(dto.SenhaHash);
		usuario.Ativo = dto.Ativo;
		usuario.AtualizadoEm = dto.AtualizadoEm;

		// Atualizar perfil (exemplo simples: substitui todas)
		if (dto.PerfilIds != null)
		{
			usuario.Perfil.Clear();
			foreach (var funcId in dto.PerfilIds)
			{
				var perfil = await _perfilRepository.GetByIdAsync(funcId);
				if (perfil != null)
				{
					usuario.Perfil.Add(perfil);
				}
			}
		}

		await _usuarioRepository.UpdateAsync(usuario);
		var resultDto = MapToDto(usuario);
		return Result<UsuarioDto>.Success(resultDto);
		// return Result.Success();
	}

	public async Task<Result> AddPerfilAsync(Guid usuarioId, Guid perfilId)
	{
		var perfil = await _perfilRepository.GetByIdAsync(usuarioId);
		var usuario = await _usuarioRepository.GetByIdAsync(perfilId);
		
		if (perfil == null) return Result.Failure(new[] { "Perfil não encontrado." });
		if (usuario == null) return Result.Failure(new[] { "Usuario não encontrado." });

		await _usuarioRepository.AddPerfilAsync(usuarioId, perfilId);
		return Result.Success();
	}

	public async Task<Result> RemovePerfilAsync(Guid usuarioId, Guid perfilId)
	{
		// Validações podem ser feitas aqui ou no repositório
		await _usuarioRepository.RemovePerfilAsync(usuarioId, perfilId);
		return Result.Success();
	}

	// Método auxiliar de mapeamento
	private UsuarioDto MapToDto(Usuario usuario)
	{
		return new UsuarioDto
		{
			Id = usuario.Id,
			Nome = usuario.Nome,
			Email = usuario.Email,
			NomeGerencia = usuario.Gerencia.NomeGerencia,
			SuperintendenciaNome = usuario.Gerencia.Superintendencia,
			Matricula = usuario.Matricula,
			SenhaHash = BC.HashPassword(usuario.SenhaHash),
			Ativo = usuario.Ativo,
			CriadoEm = usuario.CriadoEm,
			AtualizadoEm = usuario.AtualizadoEm,
			Perfil = usuario.Perfil?.Select(t => new PerfilDto
			{
				Id = t.Id,
				Nome = t.Nome,
				Descricao = t.Descricao
			}).ToList()
		};
	}
}



