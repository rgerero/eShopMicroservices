namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal price)
		: ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);
	internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			//to do
			//create product entity
			var product = new Product
			{
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				ImageFile = command.ImageFile,
				Price = command.price
			};

			//save to db
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);
			//return result
			return new CreateProductResult(Guid.NewGuid());
		}
	}
}
