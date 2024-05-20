using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers.CalculateQuote;

public class CalculateIndividualMapper
{
	public CalculateIndividualResDTO Map( IResult result )
	{
		return new CalculateIndividualResDTO
		{
			Id = result.Quotes[0].Books[0].Id.Value,
			Title = result.Quotes[0].Books[0].Data.Value.Title,
			Author = result.Quotes[0].Books[0].Data.Value.Author,
			Type = result.Quotes[0].Books[0].Data.Value.Type,
			BasePrice = result.Quotes[0].Books[0].BasePrice.Value,
			Discount = result.Quotes[0].Books[0].Discount!.Value,
			Increase = result.Quotes[0].Books[0].Increase!.Value,
			FinalPrice = result.Quotes[0].Books[0].SellPrice.Value,
		};
	}
}