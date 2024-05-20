using LaPinguinera.Quotes.Application.DTOs.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;

namespace LaPinguinera.Quotes.Application.Mappers.Book;

public class GetBooksResMapper
{
	public GetBooksResDTO Map( List<AbstractBook> books )
	{
		GetBooksResDTO getBooksResDTO = new()
		{
			Books = books.Select( book => new BookDTO
			{
				Id = book.Id.Value,
				Title = book.Data.Value.Title,
				Author = book.Data.Value.Author,
				Price = book.SellPrice.Value,
				Type = book.Data.Value.Type,
			} ).ToList()
		};

		return getBooksResDTO;
	}
}
