using MediatR;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.Garages.Commands
{
    public class AddGarageDoorStatusHistoryCommand : IRequest<Unit>
    {
        /// <summary>
        ///Unique  Door Id
        /// </summary>
        public Guid DoorId { get; }

        /// <summary>
        ///Door current status
        /// </summary>
        public bool currentStatus { get; }

        /// <summary>
        /// Garage door status
        /// </summary>
        public bool LastStatus { get; set; }

        

        public AddGarageDoorStatusHistoryCommand(Guid doorId, bool newStatus, bool oldStatus)
        {
            this.DoorId = doorId;
            this.currentStatus = newStatus;
            this.LastStatus = oldStatus;
        }
    }
    public class AddGarageDoorStatusHistoryCommandHandler : IRequestHandler<AddGarageDoorStatusHistoryCommand, Unit>
    {
        private readonly IGarageRepository _garageRepository;

        public AddGarageDoorStatusHistoryCommandHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
          
        }

        public async Task<Unit> Handle(AddGarageDoorStatusHistoryCommand request, CancellationToken cancellationToken)
        {
            var doorStatusHistory = new GarageDoorStatusHistory(request.DoorId, request.currentStatus, request.LastStatus, DateTime.UtcNow);

            await _garageRepository.AddHistoricalDoorStatusLogAsync(doorStatusHistory);
            return Unit.Value;
        }
    }
}
