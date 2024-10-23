using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Generic;

public interface ICommandUseCase<T, I, C> where T : Command<I> where I : Identity
{
	IObservable<C> Execute( IObservable<T> command );
}
