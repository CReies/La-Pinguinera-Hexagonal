namespace Hexagonal.Domain.Generic;

public class AggregateRoot<I>( I identity ) : Entity<I>( identity ) where I : Identity
{
	private readonly ChangeEventSubscriber _changeEventSubscriber = new();

	public List<DomainEvent> GetUncommittedChanges()
	{
		return [.. _changeEventSubscriber.Events];
	}

	public void MarkAsCommitted()
	{
		_changeEventSubscriber.Events.Clear();
	}

	protected void Subscribe( Behavior eventChange )
	{
		_changeEventSubscriber.Subscribe( eventChange );
	}

	protected void Apply( DomainEvent domainEvent )
	{
		_changeEventSubscriber.Apply( domainEvent );
	}

	protected Action AppendEvent( DomainEvent domainEvent )
	{
		string aggregateName = GetType().Name.ToLower();
		domainEvent.AggregateName = aggregateName;
		domainEvent.AggregateId = Id.Value;

		return _changeEventSubscriber.Append( domainEvent );
	}
}
