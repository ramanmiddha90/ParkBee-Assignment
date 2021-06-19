using MediatR;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.Garages.Queries
{
    public class GetGarageQueryByGarageId : IRequest<GarageDetail>
    {
        public Guid GarageId { get; private set; }

        public GetGarageQueryByGarageId(Guid garageId)
        {
            this.GarageId = garageId;
        }
    }
    public class GetGaragesQueryByGarageIdHandler : IRequestHandler<GetGarageQueryByGarageId, GarageDetail>
    {
        private readonly IGarageRepository _garageRepository;

        public GetGaragesQueryByGarageIdHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public async Task<GarageDetail> Handle(GetGarageQueryByGarageId request, CancellationToken cancellationToken)
        {
            return await _garageRepository.GetGarageDetailByIdAsync(request.GarageId);
        }
    }
}
