using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linnworks.Core.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.RegionId);

            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(e => e.Region)
                .WithMany(e => e.Countries)
                .HasForeignKey(e => e.RegionId);
        }
    }
}
