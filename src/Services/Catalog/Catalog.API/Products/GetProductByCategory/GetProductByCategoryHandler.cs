
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
	public record GetProductByCategoryResult(IEnumerable<Product> Products);
	public class GetProductByCategoryQueryHandler(IDocumentSession session)
		: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			var products = await session.Query<Product>().ToListAsync(cancellationToken);

			//if (products == null)
			//{
			//	throw new ProductNotFoundException();
			//}

			return new GetProductByCategoryResult(products);
		}
	}
}
