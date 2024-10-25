﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
	public class CacheBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
	{

		public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
		{
			var cacheBasket=await cache.GetStringAsync(userName, cancellationToken);

			if (!string.IsNullOrEmpty(cacheBasket))
				return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;

			var basket= await repository.GetBasket(userName, cancellationToken);
			await cache.SetStringAsync(userName,JsonSerializer.Serialize(basket),cancellationToken);
			return basket;
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
		{
			await repository.StoreBasket(basket, cancellationToken);
			await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
			return basket;
		}
		public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
		{
			await repository.DeleteBasket(userName, cancellation);
			await cache.RemoveAsync(userName, cancellation);
			return true;
		}
	}
}
