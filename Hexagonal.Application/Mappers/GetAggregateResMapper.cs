using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Mappers;

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
