using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionstring = configuration.GetConnectionString("DB");
			services.AddDbContext<ApplicationDBContext>(opt=>opt.UseSqlServer(connectionstring));
			return services;
		}
	}
}
