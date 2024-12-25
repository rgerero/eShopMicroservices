using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCarter();
			services.AddExceptionHandler<CustomExceptionHandler>();
			services.AddHealthChecks()
				.AddSqlServer(configuration.GetConnectionString("DB")!);
			return services;
		}

		public static WebApplication UseAPIServices(this WebApplication app)
		{
			app.MapCarter();
			app.UseExceptionHandler(opt => { });
			app.UseHealthChecks("/health",
				new HealthCheckOptions
				{
					ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
				});
			return app;
		}
	}
}
