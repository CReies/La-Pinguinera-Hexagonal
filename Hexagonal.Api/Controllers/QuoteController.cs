using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using LaPinguinera.Quotes.Infrastructure.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;

namespace LaPinguinera.Quotes.Infrastructure.Api.Controllers;

[ApiController]
[Route( "api/v1/quote" )]
public class QuoteController : ControllerBase
{
	[HttpPost( "individual" )]
	public async Task<IActionResult> CalculateIndividual
	(
		[FromBody] CalculateIndividualReqDTO request,
		[FromServices] ICommandUseCase<CalculateIndividualCommand, QuoteId, CalculateIndividualResDTO> useCaseCommand
	)
	{
		try
		{
			CalculateIndividualCommand command = new(
				request.AggregateId,
				request.Title,
				request.Author,
				request.Price,
				request.Type
			);
			IObservable<CalculateIndividualCommand> subject = Observable.Return( command );
			IObservable<CalculateIndividualResDTO> resultObservable = useCaseCommand.Execute( subject );
			CalculateIndividualResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}

	[HttpPost( "list" )]
	public async Task<IActionResult> CalculateList
	(
		[FromBody] CalculateListReqDTO request,
		[FromServices] ICommandUseCase<CalculateListCommand, QuoteId, CalculateListResDTO> useCaseCommand
	)
	{
		try
		{
			List<(string bookId, int quantity)> bookIdWithQuantity = request.Books
				.Select( b => (bookId: b.BookId, quantity: b.Quantity) )
				.ToList();
			CalculateListCommand command = new(
				request.AggregateId,
				bookIdWithQuantity,
				request.CustomerRegisterDate
			);
			IObservable<CalculateListCommand> subject = Observable.Return( command );
			IObservable<CalculateListResDTO> resultObservable = useCaseCommand.Execute( subject );
			CalculateListResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}

	[HttpPost( "budget" )]
	public async Task<IActionResult> CalculateBudget
	(
		[FromBody] CalculateBudgetReqDTO request,
		[FromServices] ICommandUseCase<CalculateBudgetCommand, QuoteId, CalculateBudgetResDTO> useCaseCommand
	)
	{
		try
		{
			CalculateBudgetCommand command = new(
				request.AggregateId,
				request.BookIds,
				request.Budget,
				request.CustomerRegisterDate
			);
			IObservable<CalculateBudgetCommand> subject = Observable.Return( command );
			IObservable<CalculateBudgetResDTO> resultObservable = useCaseCommand.Execute( subject );
			CalculateBudgetResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}

	[HttpPost( "group" )]
	public async Task<IActionResult> CalculateGroup
(
	[FromBody] CalculateGroupReqDTO request,
	[FromServices] ICommandUseCase<CalculateGroupCommand, QuoteId, CalculateGroupResDTO> useCaseCommand
)
	{
		try
		{
			List<List<(string bookId, int quantity)>> bookIdWithQuantityGroup = request.Group
				.Select( g => g
					.Select( b => (bookId: b.BookId, quantity: b.Quantity) )
					.ToList() )
				.ToList();
			CalculateGroupCommand command = new(
				request.AggregateId,
				bookIdWithQuantityGroup,
				request.CustomerRegisterDate
			);
			IObservable<CalculateGroupCommand> subject = Observable.Return( command );
			IObservable<CalculateGroupResDTO> resultObservable = useCaseCommand.Execute( subject );
			CalculateGroupResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}
}
