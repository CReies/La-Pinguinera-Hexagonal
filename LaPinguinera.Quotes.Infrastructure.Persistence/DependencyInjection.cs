using LaPinguinera.Quotes.Application.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace LaPinguinera.Quotes.Infrastructure.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence( this IServiceCollection services, IConfiguration configuration )
	{

		_ = services.AddScoped( ( options ) =>
		{
			MongoClient client = new( configuration["Database:ConnectionString"] );
			IMongoDatabase database = client.GetDatabase( configuration["Database:DatabaseName"] );
			return database.GetCollection<Event>( configuration["Database:CollectionName"] );
		} );

		_ = services.AddScoped<IEventsRepository, EventsRepository>();

		return services;
	}
}
