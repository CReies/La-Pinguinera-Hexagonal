using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Shared;

public class Result : IResult
{
	public List<IGroupQuote> Quotes { get; set; } = [];
	public decimal TotalPrice { get; set; }
	public decimal TotalDiscount { get; set; }
}
