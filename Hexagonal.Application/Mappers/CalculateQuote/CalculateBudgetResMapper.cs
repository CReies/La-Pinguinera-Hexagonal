using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.BudgetQuoteCalculate;

namespace LaPinguinera.Quotes.Application.Mappers.CalculateQuote;

public class CalculateBudgetResMapper
{
	public CalculateBudgetResDTO Map( Result result )
	{
		return new CalculateBudgetResDTO
		{
			Books = result.Value.Books.Select( b => new CalculateIndividualResDTO
			{
				Id = b.Id,
				Title = b.Title,
				Author = b.Author,
				Type = b.BookType,
				BasePrice = b.BasePrice!,
				Discount = b.Discount!,
				Increase = b.Increase!,
				FinalPrice = b.FinalPrice!,
			} ).ToList(),
			TotalBooks = result.Value.TotalBooks,
			TotalNovels = result.Value.TotalNovels,
			TotalBasePrice = result.Value.TotalBasePrice,
			TotalDiscount = result.Value.TotalDiscount,
			TotalIncrease = result.Value.TotalIncrease,
			TotalPrice = result.Value.TotalPrice,
			RestOfBudget = result.Value.RestOfBudget,
		};
	}
}
