using MediatR;
using ParkBee.Core.Application.Garages.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ParkBee.Core.Domain.Exceptions;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;

namespace ParkBee.Core.Application.Garages.Commands
{
    public class RefreshGarageDoorStatusCommand : IRequest<Unit>
    {
        public RefreshGarageDoorStatusCommand() { }
        /// <summary>
        /// GarageId
        /// </summary>
        public Guid GargeId { get; set; }
        /// <summary>
        /// Garage door status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///Unique  Door Id
        /// </summary>
        public Guid DoorId { get; set; }

        /// <summary>
        ///Unique  Door Id
        /// </summary>
        public string IPAddress { get; set; }

        public RefreshGarageDoorStatusCommand(Guid GarageId, Guid doorId, string IpAderess, bool Status)
        {
            this.GargeId = GarageId;
            this.DoorId = doorId;
            this.IPAddress = IpAderess;
            this.Status = Status;
        }


        public class RefreshGarageDoorStatusCommandHandler : IRequestHandler<RefreshGarageDoorStatusCommand>
        {
            private readonly IGarageRepository _garageRepository;

            public RefreshGarageDoorStatusCommandHandler(IGarageRepository garageRepository)
            {
                _garageRepository = garageRepository ?? throw new ArgumentNullException(nameof(garageRepository));
            }
            public async Task<Unit> Handle(RefreshGarageDoorStatusCommand request, CancellationToken cancellationToken)
            {
                //get door based on id
                var door = (await _garageRepository.GetGarageDoors(request.GargeId)).Where(t => t.DoorId == request.DoorId).FirstOrDefault();

                //if door is not found then we cannot update its status
                if (door == null) throw new DoorNotFoundException($"Door not found with door id {request.DoorId}");

                //not check door has valid ip 
                var hasValidIpAddress = door.IPAddress == request.IPAddress;
                var lastStatus = door.IsActive;
                //door not found with valid ip
                if (!hasValidIpAddress)
                {

                    //retry to fetch door two more times - fetch doors based on ip address now as door is null based in id
                    var retryCount = 0;
                    Door doorWithValidIp = null;
                    while (retryCount < 2 && door.IPAddress != request.IPAddress)
                    {
                        //try to fetch door with valid ip address 
                        doorWithValidIp = await _garageRepository.GetGarageDoorsByIPAddressAsync(request.DoorId, request.IPAddress);
                        //if found then check status
                        if (doorWithValidIp != null && doorWithValidIp.IPAddress == request.IPAddress)
                        {
                            door = doorWithValidIp;
                            break;
                        }
                        retryCount++;
                    }
                    //if not valid door found with ip then just set door offline then throw exception
                    if (doorWithValidIp == null)
                        door.UpdateDoorStatus(false);
                    else
                        door.UpdateDoorStatus(request.Status);
                }
                else
                {
                    door.UpdateDoorStatus(request.Status);
                }

                if (lastStatus != door.IsActive)
                    await _garageRepository.AddHistoricalDoorStatusLogAsync(new GarageDoorStatusHistory(request.DoorId, door.IsActive, lastStatus, DateTime.UtcNow));

                return Unit.Value;
            }
        }
    }
}

