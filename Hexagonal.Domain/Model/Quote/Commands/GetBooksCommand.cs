using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Commands;

public class GetBooksCommand( string quoteId ) : Command<QuoteId>( QuoteId.Of( quoteId ) )
{ }
