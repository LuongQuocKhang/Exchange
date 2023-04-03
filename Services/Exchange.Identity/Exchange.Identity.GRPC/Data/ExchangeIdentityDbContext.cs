using Exchange.Identity.GRPC.Model;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Identity.GRPC.Data
{
    public class ExchangeIdentityDbContext : DbContext
    {
        public DbSet<TBL_ADM_JWT_WHITE_LIST> JWTWhiteLists { get; set; }

        public ExchangeIdentityDbContext(DbContextOptions<ExchangeIdentityDbContext> options)
       : base(options)
        { }
    }
}
