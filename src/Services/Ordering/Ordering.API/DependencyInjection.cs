namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAPIServices(this IServiceCollection services)
		{
			// services addCarter()
			return services;
		}

		public static WebApplication UseAPIServices(this WebApplication webApplication)
		{
			// app mapcarter()
			return webApplication;
		}
	}
}
