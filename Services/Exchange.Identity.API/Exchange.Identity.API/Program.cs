using Exchange.Identity.API.Configuration;
using Exchange.Identity.API.GrpcService;
using Exchange.Identity.GRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<AccountProtoService.AccountProtoServiceClient>
                (o => o.Address = new Uri(builder.Configuration["GrpcSettings:ExchangeIdentityGRPC"]));
builder.Services.AddScoped<ExchangeIdentityGrpcService>();

var firebase_config = builder.Configuration["FIREBASE_CONFIG"] ?? throw new InvalidOperationException("Connection string 'FIREBASE_CONFIG' not found.");

// Add config for database
builder.Services.AddFirebaseConfiguration(firebase_config);
builder.Services.AddServiceConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
