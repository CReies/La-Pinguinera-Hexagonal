namespace LaPinguinera.Quotes.Application.Generic;

public interface IGetUseCase<T>
{
	IObservable<T> Execute();
}
