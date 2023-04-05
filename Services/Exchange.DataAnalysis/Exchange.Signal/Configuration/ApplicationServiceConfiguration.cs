using Exchange.Data.Repositories;

namespace Exchange.Data.Configuration
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ISignalService, SignalService>();

            return services;
        }
    }
}
