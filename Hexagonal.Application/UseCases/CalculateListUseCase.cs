using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Application.Mappers;
using LaPinguinera.Quotes.Domain.Model.Quote;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LaPinguinera.Quotes.Application.UseCases;

public class CalculateListUseCase( IEventsRepository repository )
	: ICommandUseCase<CalculateListCommand, QuoteId, CalculateListResDTO>
{
	private readonly IEventsRepository _repository = repository;

	public IObservable<CalculateListResDTO> Execute( IObservable<CalculateListCommand> commandObservable )
	{
		return commandObservable.SelectMany( command =>

		_repository.FindByAggregateId( command.AggregateId.Value )
			.ToObservable()
			.SelectMany(
			events =>
			{
				Quote quote = Quote.From( command.AggregateId.Value, events );
				quote.CalculateList( command.Books, command.CustomerRegisterDate );

				List<LaPinguinera.Domain.Generic.DomainEvent> domainEvents = quote.GetUncommittedChanges().ToList();
				CalculateListResMapper mapper = new();

				return domainEvents.ToObservable()
					.SelectMany( domainEvent => _repository.Save( domainEvent ).ToObservable() )
					.ToList()
					.Do( _ => quote.MarkAsCommitted() )
					.Select( _ => mapper.Map( quote.Result ) );
			} )
		);
	}
}
