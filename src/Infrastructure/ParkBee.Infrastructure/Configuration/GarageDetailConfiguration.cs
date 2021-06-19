using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Core.Domain.GarageAggregate.Entities;

namespace ParkBee.Infrastructure.Configuration
{
    public class GarageDetailConfiguration : IEntityTypeConfiguration<GarageDetail>
    {
        public void Configure(EntityTypeBuilder<GarageDetail> builder)
        {
            builder.ToTable("Garages");
            builder.HasKey(x => x.GarageId);
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.GarageName).HasColumnName("GarageName");
            builder.Property(x => x.GarageName);
           
            builder.HasMany(x => x.Doors).WithOne();
        }
    }
}
