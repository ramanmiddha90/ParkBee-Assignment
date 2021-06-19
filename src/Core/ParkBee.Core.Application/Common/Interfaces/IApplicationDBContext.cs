using Microsoft.EntityFrameworkCore;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Core.Application.Common.Interfaces
{
   public interface IApplicationDBContext
    {
        DbSet<GarageDetail> GarageDetails { get; set; }
        DbSet<Door> Doors { get; set; }
        DbSet<GarageDoorStatusHistory> DoorsStatusHistory { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
