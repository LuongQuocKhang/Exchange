using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder.Extensions;

namespace Exchange.Identity.API.Configuration
{
    public static class FirebaseConfiguration
    {
        public static void AddFirebaseConfiguration(this IServiceCollection services, string firebase_config)
        {
            services.AddSingleton(FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(firebase_config)
            }));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (option) =>
            //{

            //});
        }
    }
}
