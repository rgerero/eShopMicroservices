﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
	public class AuditableEntityInterceptor:SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntities(eventData.Context);
			return base.SavingChanges(eventData, result);
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context);
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private void UpdateEntities(DbContext? context)
		{
			if (context == null) return;

			foreach (var entry in context.ChangeTracker.Entries<IEntity>())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedAt = DateTime.UtcNow;
					entry.Entity.CreatedBy = "alfred";
				}

				if (entry.State == EntityState.Modified || entry.State == EntityState.Added || entry.HasChangedOwnedEntities())
				{
					entry.Entity.LastModified = DateTime.UtcNow;
					entry.Entity.LastModifiedBy = "alfred";
				}
			}
		}
	}
}

public static class Extensions
{
	public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
		entry.References.Any(r => 
			r.TargetEntry != null &&
			r.TargetEntry.Metadata.IsOwned() && 
			(r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
