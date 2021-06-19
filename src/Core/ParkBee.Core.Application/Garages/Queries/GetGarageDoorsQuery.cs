using MediatR;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.Garages.Queries
{
    public class GetGarageDoorsQuery : IRequest<List<Door>>
    {
        public Guid GarageId { get; private set; }

        public GetGarageDoorsQuery(Guid GarageId)
        {
            this.GarageId = GarageId;
        }
    }

    public class GetGargesDoorHandler : IRequestHandler<GetGarageDoorsQuery, List<Door>>
    {
        
        private readonly IGarageRepository _garageRepository;

        public GetGargesDoorHandler(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }
        public async Task<List<Door>> Handle(GetGarageDoorsQuery request, CancellationToken cancellationToken)
        {
            return await _garageRepository.GetGarageDoors(request.GarageId);
        }
    }
}
