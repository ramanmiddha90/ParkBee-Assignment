using MediatR;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.Garages.Queries
{
    /// <summary>
    /// Get garage door by Ip
    /// </summary>
    public class GetGargesDoorQueryByIp : IRequest<Door>
    {
        public string DoorIPAddress { get; private set; }

        public Guid DoorId { get; private set; }
        public GetGargesDoorQueryByIp(string doorIpAddress,Guid doorId)
        {
            this.DoorIPAddress = doorIpAddress;
            this.DoorId = doorId;
        }
    }
    public class GetGargesDoorQueryByIpHandler : IRequestHandler<GetGargesDoorQueryByIp, Door>
    {
        private readonly IGarageRepository _garageRepository;

        public GetGargesDoorQueryByIpHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }
        public async Task<Door> Handle(GetGargesDoorQueryByIp request, CancellationToken cancellationToken)
        {
            return await _garageRepository.GetGarageDoorsByIPAddressAsync(request.DoorId, request.DoorIPAddress);
        }
    }
}
