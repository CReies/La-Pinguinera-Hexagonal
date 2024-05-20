using LaPinguinera.Quotes.Application;
using LaPinguinera.Quotes.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddPersistence( builder.Configuration );

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	_ = app.UseSwagger();
	_ = app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
