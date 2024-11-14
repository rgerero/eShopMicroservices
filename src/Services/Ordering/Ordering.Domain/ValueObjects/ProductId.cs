namespace Ordering.Domain.ValueObjects
{
	public record ProductId
	{
        public Guid Value { get; }
		private ProductId(Guid value) => this.Value = value;
		public static ProductId Of(Guid value)
		{
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
			{
				throw new DomainException("ProductId cannot be empty.");
			}

			return new ProductId(value);
		}
	}
}
