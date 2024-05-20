using LaPinguinera.Quotes.Application.DTOs.Quote;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;

namespace LaPinguinera.Quotes.Infrastructure.Api.Controllers;

[ApiController]
[Route( "api/v1/secret" )]
public class QuoteController : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateQuote
	(
		[FromServices] IInitialCommandUseCase<CreateQuoteCommand, CreateQuoteResDTO> useCaseCommand,
		CreateQuoteCommand command
	)
	{
		try
		{
			IObservable<CreateQuoteCommand> subject = Observable.Return( command );
			IObservable<CreateQuoteResDTO> resultObservable = useCaseCommand.Execute( subject );
			CreateQuoteResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}

	[HttpGet]
	public async Task<IActionResult> GetAggregate
	(
				[FromServices] IGetUseCase<GetAggregateResDTO> useCase
			)
	{
		try
		{
			IObservable<GetAggregateResDTO> resultObservable = useCase.Execute();
			GetAggregateResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}
}
