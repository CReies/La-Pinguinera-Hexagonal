using LaPinguinera.Application.Generic;
using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LaPinguinera.Quotes.Application.UseCases;

public class CalculateBudgetUseCase( IEventsRepository repository )
	: ICommandUseCase<CalculateBudgetCommand, QuoteId>
{
	private readonly IEventsRepository _repository = repository;

	public IObservable<List<DomainEvent>> Execute( IObservable<CalculateBudgetCommand> commandObservable )
	{
		return commandObservable.SelectMany( command =>

		_repository.FindByAggregateId( command.AggregateId.Value )
			.ToObservable()
			.SelectMany(
			events =>
			{
				var quote = Quote.From( command.AggregateId.Value, events );
				quote.CalculateBudget( command.BookIds, command.Budget, command.CustomerRegisterDate );

				var domainEvents = quote.GetUncommittedChanges().ToList();

				return domainEvents.ToObservable()
					.SelectMany( domainEvent => _repository.Save( domainEvent ).ToObservable() )
					.ToList()
					.Do( _ => quote.MarkAsCommitted() )
					.Select( /* TODO: map the response */ _ => domainEvents );
			} )
		);
	}
}
