using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Core.Domain.GarageAggregate.ValueObjects;


namespace ParkBee.Infrastructure.Configuration
{
    class AddressesConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(x => x.AddressId);
            builder.Property(k => k.PostalCode).HasColumnName("PostalCode");
            builder.Property(k => k.StreetAddress).HasColumnName("StreetAddress");
            builder.Property(k => k.CountryCode).HasColumnName("Country");
            builder.Property(k => k.City).HasColumnName("City");
        }
    }
}
