using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Application.Mappers.CalculateQuote;

public class CalculateBudgetResMapper
{
	public CalculateBudgetResDTO Map( IResult result, decimal restOfBudget )
	{
		return new CalculateBudgetResDTO
		{
			Books = result.Quotes[0].Books.Select( b => new CalculateIndividualResDTO
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
			TotalBooks = result.Quotes[0].Books.Count( b => b.Data.Value.Type == BookType.BOOK ),
			TotalNovels = result.Quotes[0].Books.Count( b => b.Data.Value.Type == BookType.NOVEL ),
			TotalBasePrice = result.Quotes[0].TotalBasePrice,
			TotalDiscount = result.Quotes[0].TotalDiscount,
			TotalIncrease = result.Quotes[0].TotalIncrease,
			TotalPrice = result.Quotes[0].TotalPrice,
			RestOfBudget = restOfBudget,
		};
	}
}
