using LMSPO.CoreBusiness.Entities;
using LMSPO.SqlServer.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LMSPO.SqlServer.Data
{
    public class LMSDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupProduct> GroupProducts { get; set; }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new PurchasedProductConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new GroupProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
