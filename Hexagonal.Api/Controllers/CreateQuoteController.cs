using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;

namespace LaPinguinera.Quotes.Infrastructure.Api.Controllers;

[ApiController]
[Route( "api/v1/secret" )]
public class CreateQuoteController : ControllerBase
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
