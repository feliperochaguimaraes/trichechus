using System.Collections.Generic;
using Trichechus.Application.Interfaces; // Ou Trichechus.Application.Common

namespace Trichechus.Application.Common
{
	public class UserContext : IUserContext
	{
		public string UserId { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string UserEmail { get; set; } = string.Empty;
		public List<string> UserRoles { get; set; } = new List<string>();
		public string UserPerfil { get; set; } = string.Empty;
		public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
	}
}
