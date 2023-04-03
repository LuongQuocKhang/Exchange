using Exchange.Edentity.Configuraion;
using Exchange.Identity.Configuraion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddIdentityServerConfiguration(builder.Configuration);
builder.Services.AddServiceConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
