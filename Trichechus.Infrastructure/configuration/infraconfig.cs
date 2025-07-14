using Microsoft.Extensions.DependencyInjection;
using Trichechus.Domain.Interfaces;

namespace Trichechus.Infrastructure.Repositories;

public static class InfraConfig
{

	public static IServiceCollection AddInfra(this IServiceCollection services)
	{
		services.AddScoped<IAtividadeRepository, AtividadeRepository>();
		// services.AddScoped<IAtividadeService, AtividadeService>();
	// 	services.AddScoped<IDbConnection>(s =>
		//   {
		// 	  var configuration = s.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
		// 	  return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
		//   });

		return services;
	}
}

