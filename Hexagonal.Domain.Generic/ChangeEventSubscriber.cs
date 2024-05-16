namespace Hexagonal.Domain.Generic;

public class ChangeEventSubscriber
{
	public List<DomainEvent> Events { get; } = [];
	private readonly HashSet<Action<DomainEvent>> _subscribers = [];
	private readonly Dictionary<string, int> _versions = [];

	public void Subscribe( Behavior eventChange )
	{
		_subscribers.UnionWith( eventChange.Subscribers );
	}

	public void Apply( DomainEvent domainEvent )
	{
		foreach (var sub in _subscribers)
		{
			sub.Invoke( domainEvent );
			int newVersion = FindNextVersion( domainEvent );
			domainEvent.Version = newVersion;
		}
	}

	public Action Append( DomainEvent domainEvent )
	{
		Events.Add( domainEvent );
		return () => Apply( domainEvent );
	}

	private int FindNextVersion( DomainEvent domainEvent )
	{
		if (_versions.ContainsKey( domainEvent.Type ))
		{
			_versions.TryGetValue( domainEvent.Type, out var version );
			return _versions[domainEvent.Type] = version + 1;
		}

		_versions.Add( domainEvent.Type, domainEvent.Version );
		return _versions[domainEvent.Type];
	}
}
