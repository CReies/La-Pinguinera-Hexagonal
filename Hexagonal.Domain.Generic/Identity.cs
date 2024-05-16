namespace LaPinguinera.Domain.Generic;

public abstract class Identity : IValueObject<string>
{
	public string Value { get; private set; }

	public Identity()
	{
		Value = Guid.NewGuid().ToString();
	}

	public Identity( string? uuid )
	{
		if (string.IsNullOrWhiteSpace( uuid ))
		{
			throw new ArgumentException( "UUID cannot be null or empty" );
		}

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
