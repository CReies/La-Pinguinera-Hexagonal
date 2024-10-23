using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

[JsonConverter( typeof( JsonStringEnumConverter ) )]
public enum BookType
{
	BOOK,
	NOVEL
}
