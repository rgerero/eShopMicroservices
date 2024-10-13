using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
		: ICommand<UpdateProductCommandResult>;
	public record UpdateProductCommandResult(bool IsSuccess);

	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required")
				.Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}
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
			product.Price = command.Price;
			
			session.Update(product);
			await session.SaveChangesAsync(cancellationToken);

			return new UpdateProductCommandResult(true);
		}
	}
}
