using BuildingBlocks.Pagination;
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
	public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
	public class GetOrders : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/orders/", async ([AsParameters] PaginationRequest request, ISender sender) =>
			{
				var result = await sender.Send(new GetOrdersQuery(request));
				var responose=result.Adapt<GetOrdersResponse>();
				return Results.Ok(responose);
			});
		}
	}
}
