using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linnworks.Core.Infrastructure.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.OrderId);

            builder.Property(e => e.SalesChannel)
                .HasMaxLength(128);

            builder.Property(e => e.ShippedAt)
                .HasColumnType("DATE");

            builder.Property(e => e.UnitPrice)
                .HasColumnType("DECIMAL(100,2)")
                .IsRequired();

            builder.Property(e => e.UnitCost)
                .HasColumnType("DECIMAL(100,2)")
                .IsRequired();

            builder.Property(e => e.TotalRevenue)
                .HasColumnType("DECIMAL(100,2)")
                .IsRequired();

            builder.Property(e => e.TotalCost)
                .HasColumnType("DECIMAL(100,2)")
                .IsRequired();

            builder.Property(e => e.TotalProfit)
                .HasColumnType("DECIMAL(100,2)")
                .IsRequired();

            builder.HasOne(e => e.Order)
                .WithOne(e => e.Sale)
                .HasForeignKey<Sale>(e => e.OrderId);

            builder.HasOne(e => e.Item)
                .WithMany(e => e.Sales);

            builder.HasOne(e => e.Country)
                .WithMany(e => e.Sales);
        }
    }
}
