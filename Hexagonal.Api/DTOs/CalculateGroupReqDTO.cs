namespace LaPinguinera.Quotes.Infrastructure.Api.DTOs;

public class CalculateGroupReqDTO
{
	public string AggregateId { get; set; }
	public List<List<BookIdWithQuantityDTO>> Group { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }
}
