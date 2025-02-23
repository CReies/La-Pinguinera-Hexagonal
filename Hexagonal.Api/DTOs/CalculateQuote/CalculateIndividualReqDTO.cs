﻿using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Infrastructure.Api.DTOs.CalculateQuote;

public class CalculateIndividualReqDTO
{
	public string AggregateId { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public decimal Price { get; set; }
	public BookType Type { get; set; }
}
