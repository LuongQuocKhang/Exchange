using Exchange.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Identity.Infrastructure
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        { }

        public DbSet<TBL_ADM_JWT_WHITE_LIST> TBL_ADM_JWT_WHITE_LIST { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "admin";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "admin";
                        break;
                    case EntityState.Deleted:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "admin";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
