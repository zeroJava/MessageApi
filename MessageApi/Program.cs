using MessageApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

GlobalAppConfig.InitialiseUserBuilder(builder);
GlobalAppConfig.InitialiseMessageBuilder(builder);
GlobalAppConfig.InitialiseTokenGenerator(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
   app.MapScalarApiReference(); // Swagger alternative, install Scalar.AspNetCore - nuget
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
