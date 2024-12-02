using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries
{
	public record GetOrdersByNameQuery(string Name):IQuery<GetOrdersByNameResult>;
	public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}
