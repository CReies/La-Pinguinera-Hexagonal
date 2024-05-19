﻿using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Application.Mappers;

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
				Price = b.FinalPrice!.Value,
			} ).ToList(),
			TotalPrice = result.Quotes[0].TotalPrice,
			TotalDiscount = result.Quotes[0].TotalDiscount,
			RestOfBudget = restOfBudget,
		};
	}
}
