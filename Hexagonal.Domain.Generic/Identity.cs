namespace Hexagonal.Domain.Generic;

public abstract class Identity : IValueObject<string>
{
	public string Value { get; private set; }

	public Identity()
	{
		Value = Guid.NewGuid().ToString();
	}

	public Identity( string uuid )
	{
		Value = uuid;
	}

	public override bool Equals( object? obj )
	{
		return obj is Identity identity && Value == identity.Value;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine( Value );
	}
}
