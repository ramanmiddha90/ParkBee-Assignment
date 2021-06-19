using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Core.Domain.GarageAggregate.Entities;


namespace ParkBee.Infrastructure.Configuration
{
    class GarageStatusHistoryConfiguration : IEntityTypeConfiguration<GarageDoorStatusHistory>
    {
        public void Configure(EntityTypeBuilder<GarageDoorStatusHistory> builder)
        {
            builder.ToTable("GarageDoorStatusHistory");
            builder.HasKey(x => x.Id);
            
        }
    }
}
