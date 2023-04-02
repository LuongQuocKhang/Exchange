using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Exchange.Edentity.Configuraion
{
    public static class IdentityServerConfiguration
    {
        public static IServiceCollection AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            
            var IdentityResources = new List<IdentityResource>
            {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
            };

            IEnumerable<ApiScope> ApiScopes =
            new List<ApiScope>
            {
                new ApiScope(configuration["Identity_Server:ApiScope"], 
                configuration["Identity_Server:ApiScope_value"])
            };

            IEnumerable<Client> Clients =
            new List<Client>
            {
                new Client
                {
                    ClientId = configuration["Identity_Server:ClientId"],

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret(configuration["Identity_Server:ClientSecret"].Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        configuration["Identity_Server:ApiScope"]
                    }
                },
                new Client
                   {
                       ClientId = "exchange_mvc_client",
                       ClientName = "Exchange MVC Web App",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:8002/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:8002/signout-callback-oidc"
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret(configuration["Identity_Server:ClientSecret"].Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "ExchangeAPI",
                           "roles"
                       }
                   }
            };

            services.AddIdentityServer()
                    .AddInMemoryApiScopes(ApiScopes)
                    .AddInMemoryClients(Clients)
                    .AddInMemoryIdentityResources(IdentityResources);


            return services;
        }
    }
}
