using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Mappers;

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
