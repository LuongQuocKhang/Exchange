using Exchange.WebApp.HttpHandlers;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion")))
{
    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"js", @"ej2")))
    {
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"js", @"ej2"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"scripts", @"ej2.min.js"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"js", @"ej2", @"ej2.min.js"));
    }
    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2")))
    {
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"bootstrap.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"bootstrap.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"fluent.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"fluent.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"bootstrap4.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"bootstrap4.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"material.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"material.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"highcontrast.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"highcontrast.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"fabric.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"fabric.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"tailwind.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"tailwind.css"));
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp", @"node_modules", @"@syncfusion", @"ej2-js-es5", @"styles", @"bootstrap5.css"), Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", @"css", @"ej2", @"bootstrap5.css"));
    }
}

builder.Services.AddHttpClient("ExchangeAPIClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiGateWayURL"]); // API GATEWAY URL
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];

    options.ClientId = "movies_mvc_client";
    options.ClientSecret = "secret";
    options.ResponseType = "code id_token";

    //options.Scope.Add("openid");
    //options.Scope.Add("profile");
    options.Scope.Add("address");
    options.Scope.Add("email");
    options.Scope.Add("roles");

    options.ClaimActions.DeleteClaim("sid");
    options.ClaimActions.DeleteClaim("idp");
    options.ClaimActions.DeleteClaim("s_hash");
    options.ClaimActions.DeleteClaim("auth_time");
    options.ClaimActions.MapUniqueJsonKey("role", "role");

    options.Scope.Add("movieAPI");

    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = JwtClaimTypes.GivenName,
        RoleClaimType = JwtClaimTypes.Role
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
