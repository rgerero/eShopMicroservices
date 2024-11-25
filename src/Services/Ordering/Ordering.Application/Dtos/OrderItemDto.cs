namespace Ordering.Application.Dtos
{
	public record OrderItemDto(Guid Id, Guid CustomerId, int Quantity, decimal Price);
}
