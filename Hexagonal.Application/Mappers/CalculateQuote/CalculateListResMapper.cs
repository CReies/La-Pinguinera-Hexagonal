using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers.CalculateQuote;

public class CalculateListResMapper
{
	public CalculateListResDTO Map( IResult result )
	{
		return new CalculateListResDTO
		{
			Books = result.Quotes[0].Books.Select( book => new CalculateIndividualResDTO
			{
				Id = book.Id.Value,
				Title = book.Data.Value.Title,
				Author = book.Data.Value.Author,
				Type = book.Data.Value.Type,
				BasePrice = book.SellPrice!.Value,
				Discount = book.Discount!.Value,
				Increase = book.Increase!.Value,
				FinalPrice = book.FinalPrice!.Value,
			} ).ToList(),
			TotalBasePrice = result.Quotes[0].TotalBasePrice,
			TotalDiscount = result.Quotes[0].TotalDiscount,
			TotalIncrease = result.Quotes[0].TotalIncrease,
			TotalPrice = result.Quotes[0].TotalPrice,
		};
	}
}
