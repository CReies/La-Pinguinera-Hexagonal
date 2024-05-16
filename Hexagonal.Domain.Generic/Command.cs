namespace Hexagonal.Domain.Generic;

public abstract class Command<T>( T aggregateId ) where T : Identity
{
	public T AggregateId { get; } = aggregateId;
}

public abstract class InitialCommand
{
}