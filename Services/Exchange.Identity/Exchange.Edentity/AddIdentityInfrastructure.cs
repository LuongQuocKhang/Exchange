using Exchange.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Identity
{
    public static class IdentityInfrastructureConfiguration
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings:IdentityConnectionString")));

            return services;
        }
    }
}
