using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.UseCases;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using Microsoft.Extensions.DependencyInjection;

namespace LaPinguinera.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication( this IServiceCollection services )
	{
		services.AddTransient<IInitialCommandUseCase<CreateQuoteCommand>, CreateQuoteUseCase>();
		services.AddTransient<ICommandUseCase<CalculateIndividualCommand, QuoteId>, CalculateIndividualUseCase>();
		services.AddTransient<ICommandUseCase<CalculateListCommand, QuoteId>, CalculateListUseCase>();
		services.AddTransient<ICommandUseCase<CalculateBudgetCommand, QuoteId>, CalculateBudgetUseCase>();
		services.AddTransient<ICommandUseCase<CalculateGroupCommand, QuoteId>, CalculateGroupUseCase>();

		return services;
	}
}
