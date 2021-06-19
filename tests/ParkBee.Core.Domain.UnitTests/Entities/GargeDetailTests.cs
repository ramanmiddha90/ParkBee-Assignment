using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using ParkBee.Core.Domain.UnitTests.SeedData;
using System;

namespace ParkBee.Core.Domain.UnitTests.Entities
{
    [TestClass]
    public class GargeDetailTests
    {
        [TestMethod]
        public void GargeDetail_Default_Instance()
        {
            // Arrange


            // Act
            var gerageDetail = new GarageDetail();
            // Assert
            gerageDetail.Should().NotBeNull();

        }
        [TestMethod]
        public void GargeDetail_Parameter_Instance()
        {
            // Arrange
            var garage1Door1Id = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f");
            // Act
            var garageDetail = SampleData.GetGarageDetails();
            // Assert
            garageDetail.Should().NotBeNull();
            garageDetail.GarageId.Should().NotBeEmpty();
            garageDetail.Doors.Should().HaveCountGreaterThan(0);
            garageDetail.Address.Should().NotBeNull();
         

        }
    }
}
