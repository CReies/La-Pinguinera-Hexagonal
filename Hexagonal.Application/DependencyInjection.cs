using LaPinguinera.Quotes.Application.DTOs.Book;
using LaPinguinera.Quotes.Application.DTOs.CalculateQuote;
using LaPinguinera.Quotes.Application.DTOs.Quote;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Application.UseCases.Book;
using LaPinguinera.Quotes.Application.UseCases.CalculateQuote;
using LaPinguinera.Quotes.Application.UseCases.Quote;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using Microsoft.Extensions.DependencyInjection;

namespace LaPinguinera.Quotes.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication( this IServiceCollection services )
	{
		_ = services.AddTransient<IInitialCommandUseCase<CreateQuoteCommand, CreateQuoteResDTO>, CreateQuoteUseCase>();
		_ = services.AddTransient<ICommandUseCase<CalculateIndividualCommand, QuoteId, CalculateIndividualResDTO>, CalculateIndividualUseCase>();
		_ = services.AddTransient<ICommandUseCase<CalculateListCommand, QuoteId, CalculateListResDTO>, CalculateListUseCase>();
		_ = services.AddTransient<ICommandUseCase<CalculateBudgetCommand, QuoteId, CalculateBudgetResDTO>, CalculateBudgetUseCase>();
		_ = services.AddTransient<ICommandUseCase<CalculateGroupCommand, QuoteId, CalculateGroupResDTO>, CalculateGroupUseCase>();
		_ = services.AddTransient<IGetUseCase<GetAggregateResDTO>, GetAggregateUseCase>();
		_ = services.AddTransient<ICommandUseCase<GetBooksCommand, QuoteId, GetBooksResDTO>, GetBooksUseCase>();

		return services;
	}
}
