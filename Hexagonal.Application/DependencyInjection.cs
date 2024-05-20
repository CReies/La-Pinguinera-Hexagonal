using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Application.UseCases;
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
		_ = services.AddTransient<IGetAggregateUseCase<GetAggregateResDTO>, GetAggregateUseCase>();

		return services;
	}
}
