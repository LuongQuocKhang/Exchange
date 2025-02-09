using Carter;
using Exchange.Trading.Api.Extensions;
using Scalar.AspNetCore;
using Exchange.Trading.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddApiVersioningForScalar();

builder.Services.AddCarter();

builder.Services.AddHttpClient();

builder.Services.AddApplicationServices(builder.Environment.IsDevelopment());

builder.Services.AddOpenApi(options =>
{
    options.UseJwtBearerAuthentication();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapCarter();

app.UseCors(cors => cors
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithDarkMode(false);
});

await app.RunAsync();
