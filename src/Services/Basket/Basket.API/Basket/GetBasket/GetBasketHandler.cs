using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
	public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;

	public record GetBasketResult(ShoppingCart Cart);

	public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
	{
		public async Task<GetBasketResult> Handle(GetBasketQuery command, CancellationToken cancellationToken)
		{
			//to do:
			//get data in the database
			var basket = await repository.GetBasket(command.UserName, cancellationToken);

			return new GetBasketResult(basket);
		}
	}
}
