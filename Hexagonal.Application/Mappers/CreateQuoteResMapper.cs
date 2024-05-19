using LaPinguinera.Quotes.Application.DTOs;

namespace LaPinguinera.Quotes.Application.Mappers;

public class CreateQuoteResMapper
{
	public CreateQuoteResDTO Map( string aggregateId )
	{
		return new CreateQuoteResDTO
		{
			AggregateId = aggregateId
		};
	}
}
