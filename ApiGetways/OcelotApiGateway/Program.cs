using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Ocelot.Values;
using OcelotApiGateway.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ApiEndPoint:IdentityServerURL"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            
        };
    });

builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle())
    .AddDelegatingHandler<JwtAuthenticationHandler>();

var app = builder.Build();

await app.UseOcelot();
app.UseAuthentication();

app.Run();
