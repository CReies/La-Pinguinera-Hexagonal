namespace LaPinguinera.Quotes.Infrastructure.Api.DTOs;

public class CalculateListReqDTO
{
	public string AggregateId { get; set; }
	public List<BookIdWithQuantityDTO> Books { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }
}
