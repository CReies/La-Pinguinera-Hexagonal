using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Shared;

public class Result() : IResult
{
	public List<IGroupQuote> Quotes { get; set; } = [new GroupQuote()];
	public decimal TotalPrice { get; set; } = 0;
	public decimal TotalBasePrice { get; set; } = 0;
	public decimal TotalDiscount { get; set; } = 0;
	public decimal TotalIncrease { get; set; } = 0;
}
