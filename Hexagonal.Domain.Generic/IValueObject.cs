namespace Hexagonal.Domain.Generic;

public interface IValueObject<T>
{
	public T Value { get; }
}
