using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using MediatR;
using ParkBee.Core.Application.Garages.Commands;
using ParkBee.Core.Domain.GarageAggregate;
using System;
using ParkBee.Core.Application.UnitTests.SeedData;
using System.Threading.Tasks;
using System.Linq;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System.Collections.Generic;
using System.Threading;
using ParkBee.Core.Domain.Exceptions;

namespace ParkBee.Core.Application.UnitTests.Garages.Commands
{
    [TestClass]
    public class RefreshGarageDoorTests
    {
        [TestMethod]
        public void CreateRefreshCommandHandler_Instance()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();

            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.Should().BeOfType<RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler>();
        }


        [TestMethod]
        public void CreateRefreshCommand_Instance()
        {
            // Arrange


            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand();
            // Assert
            mockHandler.Should().NotBeNull();
            mockHandler.Should().BeOfType<RefreshGarageDoorStatusCommand>();
        }


        [TestMethod]
        public void CreateRefreshCommand_Parameter_Instance()
        {
            // Arrange

            var doorId = Guid.NewGuid();
            var garageId = Guid.NewGuid();
            var status = true;
            var IPAddress = "Ipaddress";

            // Act
            var refreshStatusCommand = new RefreshGarageDoorStatusCommand(garageId, doorId, IPAddress, status);

            // Assert
            refreshStatusCommand.Should().NotBeNull();
            refreshStatusCommand.Should().BeOfType<RefreshGarageDoorStatusCommand>();
            refreshStatusCommand.IPAddress.Should().BeEquivalentTo(IPAddress);
            refreshStatusCommand.Status.Should().BeTrue();
            refreshStatusCommand.GargeId.Should().NotBeEmpty();
            refreshStatusCommand.DoorId.Should().NotBeEmpty();
        }

        [TestMethod]
        public void CreateRefreshCommandHandler_NullRepository_Instance()
        {
            // Arrange

            var mediatorMock = new Mock<IMediator>();
            var mockRepository = new Mock<IGarageRepository>();

            // Act
            Action action = () => new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(null);
            // Assert

            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("garageRepository");
        }

        [TestMethod]
        public void CreateRefreshCommandHandler_Door_NotExist_Invalid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var garageDetail = SampleData.GetGarageDetails();
            //mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(garageDetail.Doors.ToList()));
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(new List<Door>()));
            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            Func<Task> response = async () => await mockHandler.Handle(SampleData.GetRefreshGarageDoorStatusCommandRequest(), CancellationToken.None);
            // Assert
            response.Should().Throw<DoorNotFoundException>();
         
        }

        [TestMethod]
        public void CreateRefreshCommandHandler_ValidDoor_IPExist_StausChanged_valid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var garageDetail = SampleData.GetGarageDetails();
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(garageDetail.Doors.ToList()));
            mockRepository.Setup(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>())).Returns(Task.CompletedTask);
            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            Func<Task> response = async () => await mockHandler.Handle(SampleData.GetRefreshGarageDoorStatusCommandRequest(), CancellationToken.None);
            // Assert
            response.Should().NotThrow();
            mockRepository.Verify(x => x.GetGarageDoors(It.IsAny<Guid>()), Times.Once);
            mockRepository.Verify(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>()), Times.Once);
        }

        [TestMethod]
        public void CreateRefreshCommandHandler_ValidDoor_IPExist_StausSame_Invalid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var garageDetail = SampleData.GetGarageDetails();
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(garageDetail.Doors.ToList()));
            mockRepository.Setup(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>())).Returns(Task.CompletedTask);
            var request = SampleData.GetRefreshGarageDoorStatusCommandRequest();
            request.Status = false;
            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            Func<Task> response = async () => await mockHandler.Handle(request, CancellationToken.None);
            // Assert
            response.Should().NotThrow();
            mockRepository.Verify(x => x.GetGarageDoors(It.IsAny<Guid>()), Times.Once);
            mockRepository.VerifyNoOtherCalls();
        }
        [TestMethod]
        public void CreateRefreshCommandHandler_ValidDoor_IPNotExist_Retry_Invalid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var garageDetail = SampleData.GetGarageDetails();
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(garageDetail.Doors.ToList()));
            mockRepository.Setup(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>())).Returns(Task.CompletedTask);
            var request = SampleData.GetRefreshGarageDoorStatusCommandRequest();
            request.Status = false;
            request.IPAddress = "test";
            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            Func<Task> response = async () => await mockHandler.Handle(request, CancellationToken.None);
            // Assert
            response.Should().NotThrow();
            mockRepository.Verify(x => x.GetGarageDoors(It.IsAny<Guid>()), Times.Once);
            mockRepository.Verify(x => x.GetGarageDoorsByIPAddressAsync(It.IsAny<Guid>(), request.IPAddress), Times.AtMost(2));
            mockRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateRefreshCommandHandler_ValidDoor_IPNotExist_Retry_statusfalsealready_Invalid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var garageDetail = SampleData.GetGarageDetails();
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(garageDetail.Doors.ToList()));
            mockRepository.Setup(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>())).Returns(Task.CompletedTask);
            var request = SampleData.GetRefreshGarageDoorStatusCommandRequest();
            request.IPAddress = "test";
            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            Func<Task> response = async () => await mockHandler.Handle(request, CancellationToken.None);
            // Assert
            response.Should().NotThrow();
            mockRepository.Verify(x => x.GetGarageDoors(It.IsAny<Guid>()), Times.Once);
            mockRepository.Verify(x => x.GetGarageDoorsByIPAddressAsync(It.IsAny<Guid>(), request.IPAddress), Times.AtMost(2));
            mockRepository.Verify(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>()), Times.Never);
            mockRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateRefreshCommandHandler_ValidDoor_IPNotExist_Retry_statusTruealready_Invalid()
        {
            // Arrange

            var mockRepository = new Mock<IGarageRepository>();
            var garageDetail = SampleData.GetGarageDetails();
            garageDetail.Doors.ToList().ForEach(x => x.UpdateDoorStatus(true));
            mockRepository.Setup(x => x.GetGarageDoors(It.IsAny<Guid>())).Returns(Task.FromResult(garageDetail.Doors.ToList()));
            mockRepository.Setup(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>())).Returns(Task.CompletedTask);
            var request = SampleData.GetRefreshGarageDoorStatusCommandRequest();
            request.IPAddress = "test";
            // Act
            var mockHandler = new RefreshGarageDoorStatusCommand.RefreshGarageDoorStatusCommandHandler(mockRepository.Object);
            Func<Task> response = async () => await mockHandler.Handle(request, CancellationToken.None);
            // Assert
            response.Should().NotThrow();
            mockRepository.Verify(x => x.GetGarageDoors(It.IsAny<Guid>()), Times.Once);
            mockRepository.Verify(x => x.GetGarageDoorsByIPAddressAsync(It.IsAny<Guid>(), request.IPAddress), Times.AtMost(2));
            mockRepository.Verify(x => x.AddHistoricalDoorStatusLogAsync(It.IsAny<GarageDoorStatusHistory>()), Times.Once);
            mockRepository.VerifyNoOtherCalls();
        }
    }
}
