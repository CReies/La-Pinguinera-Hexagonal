using LaPinguinera.Quotes.Application.DTOs.Quote;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Application.Mappers.Quote;
using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using System.Reactive.Linq;

namespace LaPinguinera.Quotes.Application.UseCases.Quote;

public class GetAggregateUseCase( IEventsRepository eventsRepository ) : IGetUseCase<GetAggregateResDTO>
{
	private readonly IEventsRepository _repository = eventsRepository;

	public IObservable<GetAggregateResDTO> Execute()
	{
		GetAggregateResMapper mapper = new();

		return Observable
			.FromAsync( () => _repository.FindAggregateByEventType( EventType.QuoteCreated.ToString() ) )
			.Select( events =>
			{
				DomainEvent domainEvent = events.FirstOrDefault() ?? throw new KeyNotFoundException( "Aggregate not created" );
				return mapper.Map( domainEvent );
			} );
	}
}
