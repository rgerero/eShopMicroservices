using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal price)
		: ICommand<UpdateProductCommandResult>;
	public record UpdateProductCommandResult(bool IsSuccess);
	public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
	{
		public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Query}", command);
			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

			if (product == null)
			{
				throw new ProductNotFoundException();
			}

			product.Name = command.Name;
			product.Category = command.Category;
			product.Description = command.Description;
			product.ImageFile = command.ImageFile;
			product.Price = command.price;
			
			session.Update(product);
			await session.SaveChangesAsync(cancellationToken);

			return new UpdateProductCommandResult(true);
		}
	}
}
