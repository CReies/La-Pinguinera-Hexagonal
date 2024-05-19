using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers;

public class CalculateBudgetResMapper
{
	public CalculateBudgetResDTO Map( IResult result, decimal restOfBudget )
	{
		return new CalculateBudgetResDTO
		{
			Books = result.Quotes.Select( q => new CalculateIndividualResDTO
			{
				Id = q.Books[0].Id.Value,
				Title = q.Books[0].Data.Value.Title,
				Author = q.Books[0].Data.Value.Author,
				Price = q.Books[0].SellPrice.Value,
			} ).ToList(),
			TotalPrice = result.TotalPrice,
			TotalDiscount = result.TotalDiscount,
			RestOfBudget = restOfBudget,
		};
	}
}
