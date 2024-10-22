
namespace Basket.API.Data
{
	public class BasketRepository : IBasketRepository
	{

		public Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default)
		{
			throw new NotImplementedException();
		}

		public Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellation = default)
		{
			throw new NotImplementedException();
		}
		public Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
		{
			throw new NotImplementedException();
		}
	}
}
