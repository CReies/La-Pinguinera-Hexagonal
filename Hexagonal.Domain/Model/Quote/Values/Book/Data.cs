using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class Data : IValueObject<(string Title, string Author, decimal SellPrice, BookType Type)>
{
    public (string Title, string Author, decimal SellPrice, BookType Type) Value { get; private set; }

    private Data((string? Title, string? Author, decimal Price, BookType Type) value)
    {
        if (string.IsNullOrWhiteSpace(value.Title))
        {
            throw new ArgumentException("Title cannot be null or empty");
        }

        if (string.IsNullOrWhiteSpace(value.Author))
        {
            throw new ArgumentException("Author cannot be null or empty");
        }

        if (value.Price <= 0)
        {
            throw new ArgumentException("Price cannot be less than or equal to zero");
        }

        Value = value!;
    }

    public static Data Of(string? Title, string? Author, decimal Price, BookType Type)
    {
        return new Data((Title, Author, Price, Type));
    }
}
