namespace LaPinguinera.Quotes.Application.Generic;

public interface IGetAggregateUseCase<T>
{
	IObservable<T> Execute();
}
