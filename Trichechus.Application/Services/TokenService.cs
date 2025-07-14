// Application/Services/TokenService.cs
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trichechus.Application.DTOs;

namespace Trichechus.Application.Services;

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	// Método para Usuario SGA (sem alterações)
	public UsuarioTokenDTO GerarToken(UsuarioSGA usuario)
	{
		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, usuario.Nome!),
				new Claim(ClaimTypes.Email, usuario.Email!),
				new Claim("PerfilSGA", usuario.Perfil!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

		// Adicionar roles como claims
		foreach (var role in usuario.Roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		return GerarTokenComClaims(claims, usuario.Nome!, usuario.Email!, usuario.Roles, usuario.Perfis);
	}

	// *** NOVO MÉTODO para Usuario Local (via UsuarioParaToken) ***
	public UsuarioTokenDTO GerarToken(UsuarioParaToken usuario)
	{
		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
				new Claim(ClaimTypes.Name, usuario.Nome!),
				new Claim(ClaimTypes.Email, usuario.Email!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				// new Claim("Perfil", usuario.Perfis), // <- importante garantir que esse existe
			};

		// Adiciona os perfis como claims
		foreach (var Perfil in usuario.Perfis)
		{
			claims.Add(new Claim("Perfil", Perfil));
		}
		// Adiciona os perfis como claims

		// Adiciona as funcionalidades como claims de Role
		foreach (var funcionalidade in usuario.Roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, funcionalidade));
		}
		// Adiciona as funcionalidades como claims de Role
		
		return GerarTokenComClaims(claims, usuario.Nome!, usuario.Email!, usuario.Roles, usuario.Perfis);
	}

	private UsuarioTokenDTO GerarTokenComClaims(List<Claim> claims, string nome, string email, List<string> roles, List<string> perfil)
	{
		// Obter chave secreta do appsettings.json
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

		// Criar credenciais de assinatura
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		// Obter tempo de expiração do appsettings.json
		var expiracao = DateTime.UtcNow.AddMinutes(
			double.Parse(_configuration["JWT:ExpiracaoMinutos"]!));

		// Criar token JWT
		var token = new JwtSecurityToken(
			issuer: _configuration["JWT:Issuer"],
			audience: _configuration["JWT:Audience"],
			claims: claims,
			expires: expiracao,
			signingCredentials: creds);

		// Retornar DTO com token e informações do usuário
		return new UsuarioTokenDTO
		{
			Token = new JwtSecurityTokenHandler().WriteToken(token),
			Expiracao = expiracao,
			Nome = nome,
			Email = email,
			Perfis = perfil,
			Roles = roles
		};
	}
}

public interface ITokenService
{
	UsuarioTokenDTO GerarToken(UsuarioParaToken usuario);
	UsuarioTokenDTO GerarToken(UsuarioSGA usuario);
}
