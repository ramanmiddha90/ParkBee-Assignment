using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;

namespace ParkBee.Core.Domain.UnitTests.Entities
{
    [TestClass]
     public class GarageDoorStatusHistoryTests
    {

        [TestMethod]
        public void GarageDoorStatusHistory_Default_Instance()
        {
            // Arrange


            // Act
            var door = new GarageDoorStatusHistory();
            // Assert
            door.Should().NotBeNull();
     
        }

        [TestMethod]
        public void GarageDoorStatusHistory_Parameter_Instance()
        {
            // Arrange
            var garage1Door1Id = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f");
            var datetime = DateTime.UtcNow;
            // Act
            var doorHistory = new GarageDoorStatusHistory(garage1Door1Id, true, true, datetime);
            // Assert
            doorHistory.Should().NotBeNull();
            doorHistory.DoorId.Should().NotBeEmpty();

            doorHistory.LastStatus.Should().BeTrue();
            doorHistory.currentStatus.Should().BeTrue();
            doorHistory.ModifiedDate.Should().BeCloseTo(datetime, 50);

        }

    }
}
