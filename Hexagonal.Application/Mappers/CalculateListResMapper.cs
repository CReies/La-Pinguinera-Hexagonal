using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers;

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
				Price = book.FinalPrice!.Value,
				Type = book.Data.Value.Type,
				Discount = book.Discount!.Value,
			} ).ToList(),
			TotalPrice = result.Quotes[0].TotalPrice,
			TotalDiscount = result.Quotes[0].TotalDiscount,
		};
	}
}
