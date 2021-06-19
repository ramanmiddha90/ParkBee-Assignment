using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
namespace ParkBee.Core.Domain.UnitTests.Entities
{
    [TestClass]
    public class DoorEntityTests
    {
        [TestMethod]
        public void Door_Default_Instance()
        {
            // Arrange


            // Act
            var door = new Door();
            // Assert
            door.Should().NotBeNull();

        }


        [TestMethod]
        public void Door_Parameter_Instance()
        {
            // Arrange
            var garage1Door1Id = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f");
            // Act
            var door = new Door(garage1Door1Id, "Human", "Human Door", true, "garage1Door1IPId");
            // Assert
            door.Should().NotBeNull();
            door.DoorId.Should().NotBeEmpty();
            door.DoorType.Should().BeEquivalentTo("Human");
            door.Description.Should().BeEquivalentTo("Human Door");
            door.IsActive.Should().BeTrue();
            door.IPAddress.Should().BeEquivalentTo("garage1Door1IPId");

        }

        [TestMethod]
        public void Door_UpdateStatus_Instance()
        {
            // Arrange
            var garage1Door1Id = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f");
            // Act
            var door = new Door(garage1Door1Id, "Human", "Human Door", true, "garage1Door1IPId");
            door.UpdateDoorStatus(false);
            // Assert
            door.Should().NotBeNull();
            door.DoorId.Should().NotBeEmpty();
            door.DoorType.Should().BeEquivalentTo("Human");
            door.Description.Should().BeEquivalentTo("Human Door");
            door.IsActive.Should().BeFalse();
            door.IPAddress.Should().BeEquivalentTo("garage1Door1IPId");

        }
    }
}
