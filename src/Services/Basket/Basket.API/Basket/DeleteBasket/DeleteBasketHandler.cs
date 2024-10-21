﻿

namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
	public record DeleteBasketResult(bool IsSuccess);
	public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
		{
			//to do
			//delete commnad

			return new DeleteBasketResult(true);

		}
	}
}
