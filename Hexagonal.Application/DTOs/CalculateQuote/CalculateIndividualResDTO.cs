﻿using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Application.DTOs.CalculateQuote;

public class CalculateIndividualResDTO
{
	public string Id { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public decimal BasePrice { get; set; }
	public decimal FinalPrice { get; set; }
	public BookType Type { get; set; }
	public decimal Discount { get; set; }
	public decimal? Increase { get; set; }
}
