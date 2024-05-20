namespace LaPinguinera.Quotes.Infrastructure.Api.DTOs.CalculateQuote;

public class CalculateListReqDTO
{
	public string AggregateId { get; set; }
	public List<BookIdWithQuantityReqDTO> Books { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }
}
