using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal price)
		: ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);
	internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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
			//return result
			return new CreateProductResult(Guid.NewGuid());
		}
	}
}
