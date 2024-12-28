﻿using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Testing;

namespace Basket.API.Basket.CheckoutBasket
{
	public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto):ICommand<CheckoutBasketResult>;
	public record CheckoutBasketResult(bool IsSuccess);
	public class CheckoutBasketCommandValidator:AbstractValidator<CheckoutBasketCommand>
	{
		public CheckoutBasketCommandValidator()
		{
			RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cant be null");
			RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
		}
	}
	public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint pubEndpoint) 
		: ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
	{
		public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
		{
			//get total price
			var basket=await repository.GetBasket(command.BasketCheckoutDto.UserName,cancellationToken);
			if (basket==null)
			{
				return new CheckoutBasketResult(false);
			}

			//set total price
			var eventMessage=command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
			eventMessage.TotalPrice = basket.TotalPrice;

			//send basket checkout
			await pubEndpoint.Publish(eventMessage, cancellationToken);

			//delete basket
			await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

			return new CheckoutBasketResult(true);
		}
	}
}
