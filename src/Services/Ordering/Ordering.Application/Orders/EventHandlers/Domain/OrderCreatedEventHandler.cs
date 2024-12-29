using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Orders.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint pubEndpoint, IFeatureManager featureManager,ILogger<OrderCreatedEventHandler> logger) 
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Doman Event handled: {DomainEvent}", domainEvent.GetType().Name);

            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
			{
				var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();

				await pubEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
			}
        }
    }
}
