using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Infrastructure.Models;

namespace Shop.Infrastructure.EntityConfigurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(o => o.Number)
                .ValueGeneratedOnAdd();
        }
    }
}
