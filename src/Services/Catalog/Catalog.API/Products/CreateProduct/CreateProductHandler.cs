﻿using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
	public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal price)
		: ICommand<CreateProductResult>;
	public record CreateProductResult(Guid Id);

	public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
			RuleFor(x => x.price).GreaterThan(0).WithMessage("Price must be greater than 0");
		}
	}
	internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator) 
		: ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			var result=await validator.ValidateAsync(command, cancellationToken);
			var error=result.Errors.Select(x=>x.ErrorMessage).ToList();
			if (error.Any())
			{
				throw new ValidationException(error.FirstOrDefault());
			}

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
