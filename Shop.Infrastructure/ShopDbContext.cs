using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Models;
using Shop.Services.Models;

namespace Shop.Infrastructure
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).GetTypeInfo().Assembly);
        }
    }
}
