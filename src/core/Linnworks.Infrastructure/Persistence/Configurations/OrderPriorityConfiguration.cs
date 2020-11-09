using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linnworks.Core.Infrastructure.Persistence.Configurations
{
    public class OrderPriorityConfiguration : IEntityTypeConfiguration<OrderPriority>
    {
        public void Configure(EntityTypeBuilder<OrderPriority> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Symbol)
                .IsUnique();

            builder.Property(e => e.Symbol)
                .HasColumnName("OrderPriority")
                .HasColumnType("NCHAR(1)")
                .IsRequired();
        }
    }
}
