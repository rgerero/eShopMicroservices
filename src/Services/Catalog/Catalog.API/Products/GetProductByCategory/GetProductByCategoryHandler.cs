﻿
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
	public record GetProductByCategoryResult(IEnumerable<Product> Products);
	public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
		: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}",query);

			var products = await session.Query<Product>().ToListAsync(cancellationToken);

			//if (products == null)
			//{
			//	throw new ProductNotFoundException();
			//}

			return new GetProductByCategoryResult(products);
		}
	}
}
