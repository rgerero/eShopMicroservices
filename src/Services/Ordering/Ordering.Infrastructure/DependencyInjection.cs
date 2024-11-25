using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionstring = configuration.GetConnectionString("DB");
			
			services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
			services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

			services.AddDbContext<ApplicationDBContext>((sp,opt) =>
			{
				opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
				opt.UseSqlServer(connectionstring);
			});

			services.AddScoped<IApplicationDbContext, ApplicationDBContext>();
			return services;
		}
	}
}
