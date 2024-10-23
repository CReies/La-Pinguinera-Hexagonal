using LaPinguinera.Quotes.Application.DTOs.Quote;
using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Mappers.Quote;

public class CreateQuoteResMapper
{
	public CreateQuoteResDTO Map( DomainEvent domainEvent )
	{
		return new CreateQuoteResDTO
		{
			AggregateId = domainEvent.AggregateId,
		};
	}
}
