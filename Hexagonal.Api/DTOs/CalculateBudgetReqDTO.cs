namespace LaPinguinera.Quotes.Infrastructure.Api.DTOs;

public class CalculateBudgetReqDTO
{
	public string AggregateId { get; set; }
	public List<string> BookIds { get; set; }
	public decimal Budget { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }
}
