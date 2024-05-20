using LaPinguinera.Quotes.Application.DTOs.Book;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using LaPinguinera.Quotes.Infrastructure.Api.DTOs.Book;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;

namespace LaPinguinera.Quotes.Infrastructure.Api.Controllers;

[ApiController]
[Route( "api/v1/books" )]
public class BookController : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> GetBooks
	(
		[FromBody] GetBooksReqDTO request,
		[FromServices] ICommandUseCase<GetBooksCommand, QuoteId, GetBooksResDTO> useCase
	)
	{
		try
		{
			GetBooksCommand command = new( request.AggregateId );

			IObservable<GetBooksCommand> subject = Observable.Return( command );
			IObservable<GetBooksResDTO> resultObservable = useCase.Execute( subject );
			GetBooksResDTO result = await resultObservable.FirstAsync();

			return Ok( result );
		}
		catch (Exception ex)
		{
			return BadRequest( ex.Message );
		}
	}
}
