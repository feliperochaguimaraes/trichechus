using System.Collections.Generic;

namespace Trichechus.Application.Interfaces
{
	public interface IUserContext
	{
		string UserId { get; set; }
		string UserName { get; set; }
		string UserEmail { get; set; }
		List<string> UserRoles { get; set; }
		string UserPerfil { get; set; }
		bool IsAuthenticated { get; }
	}
}