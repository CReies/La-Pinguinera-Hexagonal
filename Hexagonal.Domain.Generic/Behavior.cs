namespace Hexagonal.Domain.Generic;

public abstract class Behavior
{
	public HashSet<Action<DomainEvent>> Subscribers { get; private set; } = [];

	protected void AddSub<T>( Action<T> sub ) where T : DomainEvent
	{
		Subscribers.Add( (Action<DomainEvent>)sub );
	}
}
