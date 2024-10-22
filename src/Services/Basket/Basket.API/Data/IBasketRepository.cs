﻿namespace Basket.API.Data
{
	public interface IBasketRepository
	{
		Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default);
		Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellation = default);
		Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default);
	}
}
