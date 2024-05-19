using Hexagonal.Library.Quotes.Infrastructure.Persistence;
using LaPinguinera.Application.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace LaPinguinera.Infrastructure.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence( this IServiceCollection services, IConfiguration configuration )
	{

		services.AddScoped( ( options ) =>
		{
			var client = new MongoClient( configuration["Database:ConnectionString"] );
			var database = client.GetDatabase( configuration["Database:DatabaseName"] );
			return database.GetCollection<Event>( configuration["Database:CollectionName"] );
		} );

		services.AddScoped<IEventsRepository, EventsRepository>();

		return services;
	}
}
