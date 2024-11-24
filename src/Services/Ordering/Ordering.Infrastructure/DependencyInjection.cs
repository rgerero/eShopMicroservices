using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionstring = configuration.GetConnectionString("DB");
			services.AddDbContext<ApplicationDBContext>(opt =>
			{
				opt.AddInterceptors(new AuditableEntityInterceptor());
				opt.UseSqlServer(connectionstring);
			});
			return services;
		}
	}
}
