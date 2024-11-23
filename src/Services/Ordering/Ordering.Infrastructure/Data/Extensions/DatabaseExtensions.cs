﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
{
	public static class DatabaseExtensions
	{
		public static async Task InitializeDatabaseAsync(this WebApplication app)
		{
			using var scope=app.Services.CreateScope();
			var context =scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
			context.Database.MigrateAsync().GetAwaiter().GetResult();
		}
	}
}
