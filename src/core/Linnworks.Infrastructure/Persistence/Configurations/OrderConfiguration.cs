using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linnworks.Core.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.OrderedAt)
                .HasColumnType("DATE")
                .IsRequired();

            builder.HasOne(e => e.OrderPriority)
                .WithMany(e => e.Orders);
        }
    }
}
