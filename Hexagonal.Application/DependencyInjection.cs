using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Application.UseCases;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using Microsoft.Extensions.DependencyInjection;

namespace LaPinguinera.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication( this IServiceCollection services )
	{
		services.AddTransient<IInitialCommandUseCase<CreateQuoteCommand, CreateQuoteResDTO>, CreateQuoteUseCase>();
		services.AddTransient<ICommandUseCase<CalculateIndividualCommand, QuoteId, CalculateIndividualResDTO>, CalculateIndividualUseCase>();
		services.AddTransient<ICommandUseCase<CalculateListCommand, QuoteId, CalculateListResDTO>, CalculateListUseCase>();
		services.AddTransient<ICommandUseCase<CalculateBudgetCommand, QuoteId, CalculateBudgetResDTO>, CalculateBudgetUseCase>();
		services.AddTransient<ICommandUseCase<CalculateGroupCommand, QuoteId, CalculateGroupResDTO>, CalculateGroupUseCase>();

		return services;
	}
}
