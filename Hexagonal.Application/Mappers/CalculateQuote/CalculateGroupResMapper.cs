using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers.CalculateQuote;

public class CalculateGroupResMapper
{
	public CalculateGroupResDTO Map( IResult result )
	{
		return new CalculateGroupResDTO
		{
			Groups = result.Quotes.Select( q => new CalculateListResDTO
			{
				Books = q.Books.Select( b => new CalculateIndividualResDTO
				{
					Id = b.Id.Value,
					Title = b.Data.Value.Title,
					Author = b.Data.Value.Author,
					Type = b.Data.Value.Type,
					BasePrice = b.SellPrice!.Value,
					Discount = b.Discount!.Value,
					Increase = b.Increase!.Value,
					FinalPrice = b.FinalPrice!.Value,
				} ).ToList(),
				TotalBasePrice = q.TotalBasePrice,
				TotalDiscount = q.TotalDiscount,
				TotalIncrease = q.TotalIncrease,
				TotalPrice = q.TotalPrice,
			} ).ToList(),
			TotalBasePrice = result.TotalBasePrice,
			TotalDiscount = result.TotalDiscount,
			TotalIncrease = result.TotalIncrease,
			TotalPrice = result.TotalPrice,
		};
	}
}
