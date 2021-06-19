
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Core.Domain.GarageAggregate
{
    public interface IGarageRepository
    {
        Task<GarageDetail> GetGarageDetailByIdAsync(Guid userId);
        Task<Door> GetGarageDoorsByIPAddressAsync(Guid doorId,string IPAddress);
        Task<List<Door>> GetGarageDoors(Guid garageId);
        Task<bool> UpdateGarageDoorStatusAsync(Door door);
        Task AddHistoricalDoorStatusLogAsync(GarageDoorStatusHistory doorsStatusHistory);
    }
}
