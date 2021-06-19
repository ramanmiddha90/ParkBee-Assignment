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
    public class GetGarageQueryByGarageIdTests
    {

        [TestMethod]
        public void GetGaragerQuery_Instance()
        {
            // Arrange
            var garaeId = Guid.NewGuid();

            // Act
            var mockHandler = new GetGarageQueryByGarageId(garaeId);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.GarageId.Should().NotBeEmpty();
            mockHandler.Should().BeOfType<GetGarageQueryByGarageId>();
        }
        [TestMethod]
        public void GetGaragerQueryHandler_Instance()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();

            // Act
            var mockHandler = new GetGaragesQueryByGarageIdHandler(mockRepository.Object);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.Should().BeOfType<GetGaragesQueryByGarageIdHandler>();
        }
        [TestMethod]
        public void GetGaragerQueryHandler_Valid()
        {
            // Arrange
            var garaeId = Guid.NewGuid();
            var mockRepository = new Mock<IGarageRepository>();
            var garage = SampleData.GetGarageDetails();
            mockRepository.Setup(x => x.GetGarageDetailByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(garage));
            // Act
            var mockHandler = new GetGaragesQueryByGarageIdHandler(mockRepository.Object);
            Func<Task<GarageDetail>> result = async () => await mockHandler.Handle(new GetGarageQueryByGarageId(garaeId), CancellationToken.None);
            // Assert
            result.Should().NotBeNull();

            mockHandler.Should().BeOfType<GetGaragesQueryByGarageIdHandler>();
            result.Should().NotThrow();
            result.ToString().OfType<List<Door>>();
            var garageResult = result().Result;
            garageResult.Should().NotBeNull();
        }
    }
}
