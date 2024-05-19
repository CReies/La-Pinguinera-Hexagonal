using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers;

public class CalculateIndividualMapper
{
	public CalculateIndividualResDTO Map( IResult result )
	{
		return new CalculateIndividualResDTO
		{
			Id = result.Quotes[0].Books[0].Id.Value,
			Title = result.Quotes[0].Books[0].Data.Value.Title,
			Author = result.Quotes[0].Books[0].Data.Value.Author,
			Price = result.Quotes[0].Books[0].SellPrice.Value,
		};
	}
}