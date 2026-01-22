using Microsoft.EntityFrameworkCore;
using WizCo.Domain.Entities;
using WizCo.Infrastructure.Data.Mappings;

namespace WizCo.Infrastructure.Data
{
    public class WizCoDbContext : DbContext
    {
        public WizCoDbContext(DbContextOptions<WizCoDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ItemOrderMap());
        }
    }
}
