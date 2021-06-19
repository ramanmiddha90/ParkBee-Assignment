using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkBee.Core.Domain.GarageAggregate.ValueObjects;
using System;

namespace ParkBee.Core.Domain.UnitTests.ValueObjects
{
    public class AddressTests
    {

        [TestMethod]
        public void Address_Default_Instance()
        {
            // Arrange
            // Act
            var address = new Address();
            // Assert
            address.Should().NotBeNull();

        }

        [TestMethod]
        public void Door_Parameter_Instance()
        {
            // Arrange
            var addressId = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f");
            // Act
            var address = new Address(addressId, "Street", "122001", "Maastricht", "NL", "1");
            // Assert
            address.Should().NotBeNull();
            address.AddressId.Should().NotBeEmpty();
            address.StreetAddress.Should().BeEquivalentTo("Street");
            address.PostalCode.Should().BeEquivalentTo("122001");
            address.City.Should().BeEquivalentTo("Maastricht");
            address.CountryCode.Should().BeEquivalentTo("NL");
            address.ZoneNumber.Should().BeEquivalentTo("1");

        }

    }
}
