using Exchange.Identity.API.Repositories;
using Firebase.Auth.Providers;
using Firebase.Auth;

namespace Exchange.Identity.API.Configuration
{
    public static class ServiceConfiguration
    {
        public static void AddServiceConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IAccountService, AccountService>();

            var config = new FirebaseAuthConfig
            {
                ApiKey = configuration["Firebase_API_config:ApiKey"],
                AuthDomain = configuration["Firebase_API_config:AuthDomain"],
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                }
            };


            var client = new FirebaseAuthClient(config);

            services.AddSingleton<FirebaseAuthClient>(client);
        }
    }
}
