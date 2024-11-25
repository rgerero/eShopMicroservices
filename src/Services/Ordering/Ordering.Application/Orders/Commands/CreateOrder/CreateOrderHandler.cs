﻿using BuildingBlocks.CQRS;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
	{
		public Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			//create
			//save to db
			//return result
			throw new NotImplementedException();
		}
	}
}
