using Exchange.Data.Data;

namespace Exchange.Data.Configuration
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISignalContext, SignalContext>();
            return services;
        }
    }
}
