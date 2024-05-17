namespace LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

public interface IResult
{
	List<IGroupQuote> Quotes { get; }
	decimal TotalPrice { get; }
	decimal TotalDiscount { get; }
}
