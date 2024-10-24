

using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
	public record DeleteBasketResult(bool IsSuccess);
	public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
		{
			//to do
			//delete commnad

			var basket = await repository.DeleteBasket(command.UserName, cancellationToken);

			return new DeleteBasketResult(true);

		}
	}
}
