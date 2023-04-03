using Exchange.Identity.GRPC.Data;
using Exchange.Identity.GRPC.Repositories;
using Exchange.Identity.GRPC.Services;
using Firebase.Auth.Providers;
using Firebase.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

var firebase_config = builder.Configuration["FIREBASE_CONFIG"] ?? throw new InvalidOperationException("Connection string 'FIREBASE_CONFIG' not found.");

// Add config for database

#region Add Firebase Authentication Configuration
builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(firebase_config)
}));

var config = new FirebaseAuthConfig
{
    ApiKey = builder.Configuration["Firebase_API_config:ApiKey"],
    AuthDomain = builder.Configuration["Firebase_API_config:AuthDomain"],
    Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                }
};


var client = new FirebaseAuthClient(config);

builder.Services.AddSingleton<FirebaseAuthClient>(client);
#endregion

#region Add DbContext

builder.Services.AddDbContext<ExchangeIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ExchangeIdentityConnectionString")));
#endregion

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AccountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
