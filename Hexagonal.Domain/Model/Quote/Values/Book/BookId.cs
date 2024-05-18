using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class BookId : Identity
{
    public BookId() : base() { }
    private BookId(string? id) : base(id) { }
    public static BookId Of(string? id) => new(id);
}
