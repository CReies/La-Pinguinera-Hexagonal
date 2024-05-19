using LaPinguinera.Application.Generic;
using LaPinguinera.Domain.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace LaPinguinera.Quotes.Infrastructure.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence( this IServiceCollection services, IConfiguration configuration )
	{

		services.AddScoped<IMongoCollection<DomainEvent>>( ( options ) =>
		{
			var client = new MongoClient( configuration["Database:ConnectionString"] );
			var database = client.GetDatabase( configuration["Database:DatabaseName"] );
			return database.GetCollection<DomainEvent>( configuration["Database:CollectionName"] );
		} );

		services.AddScoped<IEventsRepository, EventsRepository>();

		return services;
	}
}
