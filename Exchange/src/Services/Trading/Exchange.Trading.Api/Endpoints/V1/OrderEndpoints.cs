using Asp.Versioning.Builder;
using Carter;
using MediatR;

namespace Exchange.Trading.Api.Endpoints.V1;

public class OrderEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new Asp.Versioning.ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder groupBuilder = app.MapGroup("/api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        groupBuilder.MapGet("/orders", async (CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Get open position");

        groupBuilder.MapGet("/orders/history", async (CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Get order history");

        groupBuilder.MapPost("/orders/limit", async (CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Place Limit Order");

        groupBuilder.MapPost("/orders/market", async (CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Place Market Order");

        groupBuilder.MapPut("/orders/{id}", async (int id, CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Update Order");

        groupBuilder.MapPut("/orders/{id}/cancel", async (int id, CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Cancel Order");

        groupBuilder.MapPut("/orders/{id}/close", async (int id, CancellationToken cancellationToken) =>
        {
            return Results.Created();
        })
            .WithName("Close Position");
    }
}
