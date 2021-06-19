using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkBee.Core.Application.Garages.Queries;
using ParkBee.Core.Application.UnitTests.SeedData;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.UnitTests.Garages.Queries
{
    [TestClass]
    public class GetGarageDoorsQueryTests
    {
        [TestMethod]
        public void GetGarageDoorQuery_Instance()
        {
            // Arrange
            var garaeId = Guid.NewGuid();

            // Act
            var mockHandler = new GetGarageDoorsQuery(garaeId);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.GarageId.Should().NotBeEmpty();
            mockHandler.Should().BeOfType<GetGarageDoorsQuery>();
        }


        [TestMethod]
        public void GetGargesDoorHandler_Instance()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();

            // Act
            var mockHandler = new GetGargesDoorHandler(mockRepository.Object);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.Should().BeOfType<GetGargesDoorHandler>();
        }
        [TestMethod]
        public void GetGargesDoorHandler_Valid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var sampleDoors = SampleData.GetGarageDetails().Doors;
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(sampleDoors.ToList()));
            // Act
            var mockHandler = new GetGargesDoorHandler(mockRepository.Object);
            Func<Task<List<Door>>> result = async () => await mockHandler.Handle(new GetGarageDoorsQuery(new Guid()), CancellationToken.None);
            // Assert
            result.Should().NotBeNull();

            mockHandler.Should().BeOfType<GetGargesDoorHandler>();
            result.Should().NotThrow();
            result.ToString().OfType<List<Door>>();
            var doors = result().Result;
            Assert.AreEqual(doors.Count(), 3);
        }

    }
}
