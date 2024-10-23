using LaPinguinera.Quotes.Application.DTOs.Quote;
using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Mappers.Quote;

public class GetAggregateResMapper
{
	public GetAggregateResDTO Map( DomainEvent domainEvent )
	{
		return new GetAggregateResDTO
		{
			AggregateId = domainEvent.AggregateId,
		};
	}
}
