using Exchange.Identity.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.Identity.Configuraion
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
