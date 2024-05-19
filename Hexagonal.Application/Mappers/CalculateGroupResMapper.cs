using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers;

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
					Price = b.SellPrice.Value,
				} ).ToList(),
				TotalPrice = q.TotalPrice,
				TotalDiscount = q.TotalDiscount,
			} ).ToList(),
			TotalPrice = result.TotalPrice,
			TotalDiscount = result.TotalDiscount,
		};
	}
}
