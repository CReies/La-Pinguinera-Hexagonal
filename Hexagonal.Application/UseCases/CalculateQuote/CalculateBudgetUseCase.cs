using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Application.Mappers.CalculateQuote;
using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LaPinguinera.Quotes.Application.UseCases.CalculateQuote;

public class CalculateBudgetUseCase( IEventsRepository repository )
		: ICommandUseCase<CalculateBudgetCommand, QuoteId, CalculateBudgetResDTO>
{
	private readonly IEventsRepository _repository = repository;

	public IObservable<CalculateBudgetResDTO> Execute( IObservable<CalculateBudgetCommand> commandObservable )
	{
		return commandObservable.SelectMany( command =>

		_repository.FindByAggregateId( command.AggregateId.Value )
				.ToObservable()
				.SelectMany(
				events =>
				{
					Quote quote = Quote.From( command.AggregateId.Value, events );
					quote.CalculateBudget( command.BookIds, command.Budget, command.CustomerRegisterDate );

					List<DomainEvent> domainEvents = quote.GetUncommittedChanges();
					CalculateBudgetResMapper mapper = new();

					return domainEvents.ToObservable()
									.SelectMany( domainEvent => _repository.Save( domainEvent ).ToObservable() )
									.ToList()
									.Do( _ => quote.MarkAsCommitted() )
									.Select( _ => mapper.Map( quote.Result, quote.RestBudget!.Value ) );
				} )
		);
	}
}
