namespace Hexagonal.Domain.Generic;

public abstract class Entity<I>( I identity ) where I : Identity
{
	public I Id { get; private set; } = identity ?? throw new ArgumentNullException( nameof( identity ), "The identity cannot be null" );
}
