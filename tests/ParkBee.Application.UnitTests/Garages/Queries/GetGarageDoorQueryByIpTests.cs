using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkBee.Core.Application.Garages.Queries;
using ParkBee.Core.Application.UnitTests.SeedData;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace ParkBee.Core.Application.UnitTests.Garages.Queries
{
    [TestClass]
    public class GetGarageDoorQueryByIpTests
    {

        [TestMethod]
        public void GetGarageDoorQueryByIp_Instance()
        {
            // Arrange
            var garaeId = Guid.NewGuid();
            string IpAddress = "IpAddress";

            // Act
            var mockHandler = new GetGargesDoorQueryByIp(IpAddress,garaeId);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.DoorId.Should().NotBeEmpty();
            mockHandler.DoorIPAddress.Should().BeEquivalentTo(IpAddress);
            mockHandler.Should().BeOfType<GetGargesDoorQueryByIp>();
        }
        [TestMethod]
        public void GetGarageDoorQueryByIpHandler_Instance()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();

            // Act
            var mockHandler = new GetGargesDoorQueryByIpHandler(mockRepository.Object);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.Should().BeOfType<GetGargesDoorQueryByIpHandler>();
        }
        [TestMethod]
        public void GetGarageDoorQueryByIpHandler_Valid()
        {
            // Arrange
            var garaeId = Guid.NewGuid();
            string IpAddress = "IpAddress";
            var mockRepository = new Mock<IGarageRepository>();
            var garage = SampleData.GetGarageDetails();
            mockRepository.Setup(x => x.GetGarageDoorsByIPAddressAsync(It.IsAny<Guid>(),IpAddress)).Returns(Task.FromResult(garage.Doors.First()));
            // Act
            var mockHandler = new GetGargesDoorQueryByIpHandler(mockRepository.Object);
            Func<Task<Door>> result = async () => await mockHandler.Handle(new GetGargesDoorQueryByIp(IpAddress, garaeId), CancellationToken.None);
            // Assert
            result.Should().NotBeNull();

            mockHandler.Should().BeOfType<GetGargesDoorQueryByIpHandler>();
            result.Should().NotThrow();
            result.ToString().OfType<Door>();
            var garageResult = result().Result;
            garageResult.Should().NotBeNull();
        }
    }
}
