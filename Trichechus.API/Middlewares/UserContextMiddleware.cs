// API/Middlewares/UserContextMiddleware.cs
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trichechus.Application.Interfaces;

namespace Trichechus.API.Middlewares;

public class UserContextMiddleware
{
	private readonly RequestDelegate _next;

	public UserContextMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context, IUserContext userContext)
	{
		if (context.User.Identity?.IsAuthenticated == true)
		{
			var claimsPrincipal = context.User;

			userContext.UserId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
			userContext.UserName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
			userContext.UserEmail = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
			userContext.UserRoles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

			var perfilClaim = claimsPrincipal.FindFirst("Perfil");
			userContext.UserPerfil = perfilClaim != null ? perfilClaim.Value : string.Empty;

			// Se preferir, pode lançar exceção aqui caso falte alguma claim essencial
			if (string.IsNullOrWhiteSpace(userContext.UserId))
			{
				throw new Exception("Token JWT não contém as claims obrigatórias.");
			}
		}

		await _next(context);
	}
}