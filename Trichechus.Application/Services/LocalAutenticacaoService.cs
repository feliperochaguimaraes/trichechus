using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trichechus.Application.Common;
using Trichechus.Application.DTOs;
using Trichechus.Domain.Entities;
using Trichechus.Domain.Interfaces;
using BC = BCrypt.Net.BCrypt; // Alias para BCrypt

namespace Trichechus.Application.Services;

public class LocalAutenticacaoService : ILocalAutenticacaoService
{
	private readonly IUsuarioLocalRepository _usuarioLocalRepository;
	private readonly IPerfilRepository _perfilRepository;
	private readonly ITokenService _tokenService;
	private readonly IValidator<RegistroUsuarioLocalDTO> _registroValidator;
	// O validador de LoginUsuarioDTO já deve estar registrado globalmente

	public LocalAutenticacaoService(
		IUsuarioLocalRepository usuarioLocalRepository,
		IPerfilRepository perfilRepository,
		ITokenService tokenService,
		IValidator<RegistroUsuarioLocalDTO> registroValidator)
	{
		_usuarioLocalRepository = usuarioLocalRepository;
		_perfilRepository = perfilRepository;
		_tokenService = tokenService;
		_registroValidator = registroValidator;
	}

	public async Task<Result<UsuarioTokenDTO>> RegistrarLocalAsync(RegistroUsuarioLocalDTO dto)
	{
		// Validar DTO
		var validationResult = await _registroValidator.ValidateAsync(dto);
		if (!validationResult.IsValid)
		{
			return Result<UsuarioTokenDTO>.Failure(validationResult.Errors.Select(e => e.ErrorMessage));
		}

		// Verificar se email já existe
		if (await _usuarioLocalRepository.EmailExistsAsync(dto.Email))
		{
			return Result<UsuarioTokenDTO>.Failure(new[] { "Este email já está em uso." });
		}

		// Buscar perfis solicitados (ou um perfil padrão)
		var perfisParaAdicionar = new List<Perfil>();
		if (dto.NomesPerfis != null && dto.NomesPerfis.Any())
		{
			foreach (var nomePerfil in dto.NomesPerfis)
			{
				var perfil = await _perfilRepository.GetByNameAsync(nomePerfil);
				if (perfil != null)
				{
					perfisParaAdicionar.Add(perfil);
				}
				else
				{
					// Opcional: retornar erro se um perfil não for encontrado
					// return Result<UsuarioTokenDTO>.Failure(new[] { $"Perfil '{nomePerfil}' não encontrado." });
				}
			}
		}
		else
		{
			// Adicionar um perfil padrão se nenhum for especificado
			var perfilPadrao = await _perfilRepository.GetByNameAsync("Usuario Padrao"); // Certifique-se que este perfil exista
			if (perfilPadrao != null)
			{
				perfisParaAdicionar.Add(perfilPadrao);
			}
			// Opcional: Criar o perfil padrão se não existir
		}

		// Criar novo usuário local
		var novoUsuario = new UsuarioLocal
		{
			Nome = dto.Nome,
			Email = dto.Email,
			SenhaHash = BC.HashPassword(dto.Senha),
			Ativo = true, // Ou false, se precisar de confirmação de email
			CriadoEm = DateTime.UtcNow,
			Perfis = perfisParaAdicionar
		};

		await _usuarioLocalRepository.AddAsync(novoUsuario);

		// Gerar token para o novo usuário
		var tokenResult = await GerarTokenParaUsuarioLocal(novoUsuario);
		return tokenResult;
	}

	public async Task<Result<UsuarioTokenDTO>> LoginLocalAsync(LoginUsuarioDTO dto)
	{
		// Buscar usuário pelo email (incluindo perfis e funcionalidades)
		var usuario = await _usuarioLocalRepository.GetByEmailAsync(dto.Email);

		// Verificar se usuário existe, está ativo e senha confere
		if (usuario == null || !usuario.Ativo || !BC.Verify(dto.Senha, usuario.SenhaHash))
		{
			return Result<UsuarioTokenDTO>.Failure(new[] { "Email ou senha inválidos, ou usuário inativo." });
		}

		// Gerar token
		var tokenResult = await GerarTokenParaUsuarioLocal(usuario);
		return tokenResult;
	}

	// Método auxiliar para gerar token e extrair funcionalidades como roles
	private async Task<Result<UsuarioTokenDTO>> GerarTokenParaUsuarioLocal(UsuarioLocal usuario)
	{
		if (usuario.Perfis == null || !usuario.Perfis.Any())
		{
			// Recarrega o usuário com os perfis e funcionalidades, caso não tenham sido carregados
			var usuarioCompleto = await _usuarioLocalRepository.GetByIdAsync(usuario.Id);
			if (usuarioCompleto == null)
				return Result<UsuarioTokenDTO>.Failure(new[] { "Usuário não encontrado." });

			usuario = usuarioCompleto;

			if (usuario.Perfis == null || !usuario.Perfis.Any())
				return Result<UsuarioTokenDTO>.Failure(new[] { "Usuário sem perfis atribuídos." });
		}

		// Extrair todas as funcionalidades únicas dos perfis do usuário
		var funcionalidades = usuario.Perfis
			.SelectMany(p => p.Funcionalidades ?? new List<Funcionalidade>())
			.Select(f => f.Nome)
			.Distinct()
			.ToList();
		var perfis = usuario.Perfis.Select(f => f.Nome).ToList();
		// Criar um objeto temporário para passar ao TokenService
		var usuarioParaToken = new UsuarioParaToken
		{
			Id = usuario.Id, // Necessário para claims de NameIdentifier
			Nome = usuario.Nome,
			Email = usuario.Email,
			Perfis = perfis,
			Roles = funcionalidades // Usa as funcionalidades como 'Roles'
		};

		var tokenDTO = _tokenService.GerarToken(usuarioParaToken);
		return Result<UsuarioTokenDTO>.Success(tokenDTO);
	}
}

// Classe auxiliar para passar dados ao TokenService de forma genérica
public class UsuarioParaToken
{
	public Guid Id { get; set; } // Necessário para claims de NameIdentifier
	public string? Nome { get; set; }
	public string? Email { get; set; }
	public List<string> Perfis { get; set; } = new List<string>();
	public List<string> Roles { get; set; } = new List<string>();
}
public interface ILocalAutenticacaoService
{
	Task<Result<UsuarioTokenDTO>> RegistrarLocalAsync(RegistroUsuarioLocalDTO dto);
	Task<Result<UsuarioTokenDTO>> LoginLocalAsync(LoginUsuarioDTO dto);
}
