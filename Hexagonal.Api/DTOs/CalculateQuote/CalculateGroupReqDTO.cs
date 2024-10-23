namespace LaPinguinera.Quotes.Infrastructure.Api.DTOs.CalculateQuote;

public class CalculateGroupReqDTO
{
	public string AggregateId { get; set; }
	public List<List<BookIdWithQuantityReqDTO>> Group { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }
}
