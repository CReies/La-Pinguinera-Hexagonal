using Amazon.Runtime.Internal;
using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.DTOs;
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
			var command = new CalculateIndividualCommand(
				request.AggregateId,
				request.Title,
				request.Author,
				request.Price,
				request.Type
			);
			var subject = Observable.Return( command );
			var resultObservable = useCaseCommand.Execute( subject );
			var result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}
}
