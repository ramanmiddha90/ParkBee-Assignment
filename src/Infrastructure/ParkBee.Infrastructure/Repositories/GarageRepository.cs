using ParkBee.Core.Application.Common.Interfaces;
using ParkBee.Core.Domain.GarageAggregate;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Collections.Generic;

namespace ParkBee.Infrastructure.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private IApplicationDBContext _dbContext;
        public GarageRepository(IApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }

        /// <summary>
        /// Add garage status change history
        /// </summary>
        /// <param name="doorsStatusHistory"></param>
        /// <returns></returns>
        public async Task AddHistoricalDoorStatusLogAsync(GarageDoorStatusHistory doorsStatusHistory)
        {
            await _dbContext.DoorsStatusHistory.AddAsync(doorsStatusHistory);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get garage detail by id
        /// </summary>
        /// <param name="garageId"></param>
        /// <returns></returns>

        public async Task<GarageDetail> GetGarageDetailByIdAsync(Guid garageId)
        {
            var garage = await _dbContext.GarageDetails.Include(x => x.Doors).Include(a => a.Address).Where(t => t.GarageId == garageId).FirstOrDefaultAsync();
            return garage;
        }

        public async Task<List<Door>> GetGarageDoors(Guid garageId)
        {
            var garage= await _dbContext.GarageDetails.Include(x => x.Doors).Where(t => t.GarageId == garageId).FirstOrDefaultAsync();
            return garage?.Doors.ToList();
        }

        /// <summary>
        /// Get garage doors by IP address
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        public async Task<Door> GetGarageDoorsByIPAddressAsync(Guid doorId,string IPAddress)
        {
            var garage = await _dbContext.Doors.Where(t => t.IPAddress == IPAddress && t.DoorId== doorId).FirstOrDefaultAsync();
            return garage;
        }

        /// <summary>
        /// Update garage door
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public async Task<bool> UpdateGarageDoorStatusAsync(Door door)
        {
            var rowsAffected = await _dbContext.SaveChangesAsync(CancellationToken.None);
            return rowsAffected > 0;
        }
    }
}
