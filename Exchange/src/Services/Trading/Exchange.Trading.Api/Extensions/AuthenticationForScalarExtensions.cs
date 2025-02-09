using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Exchange.Trading.Api.Extensions;

/// <summary>
/// Scalar API Extensions
/// </summary>
public static class AuthenticationForScalarExtensions
{
    /// <summary>
    /// Config Jwt Bearer for Scalar API
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static OpenApiOptions UseJwtBearerAuthentication(this OpenApiOptions options)
    {
        OpenApiSecurityScheme schema = new()
        {
            Type = SecuritySchemeType.Http,
            Name = JwtBearerDefaults.AuthenticationScheme,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        };

        options.AddDocumentTransformer((document, context, ct) =>
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes.Add(JwtBearerDefaults.AuthenticationScheme, schema);
            return Task.CompletedTask;
        });

        options.AddOperationTransformer((operation, context, ct) =>
        {
            if (context.Description.ActionDescriptor.EndpointMetadata.OfType<IAuthorizeData>().Any())
            {
                operation.Security = [new OpenApiSecurityRequirement() {
                [schema] = []
            }];
            }

            return Task.CompletedTask;
        });
        return options;
    }
}
