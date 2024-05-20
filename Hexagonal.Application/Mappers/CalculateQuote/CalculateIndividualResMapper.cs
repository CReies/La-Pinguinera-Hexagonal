using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.CreationQuoteCalculate;

namespace LaPinguinera.Quotes.Application.Mappers.CalculateQuote;

public class CalculateIndividualMapper
{
	public CalculateIndividualResDTO Map( Result result )
	{
		return new CalculateIndividualResDTO
		{
			Id = result.Value.Id,
			Title = result.Value.Title,
			Author = result.Value.Author,
			Type = result.Value.Type,
			BasePrice = result.Value.BasePrice,
			Discount = result.Value.Discount,
			Increase = result.Value.Increase!,
			FinalPrice = result.Value.FinalPrice,
		};
	}
}