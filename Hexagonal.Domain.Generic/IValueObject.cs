namespace LaPinguinera.Domain.Generic;

public interface IValueObject<T>
{
	public T Value { get; }
}
