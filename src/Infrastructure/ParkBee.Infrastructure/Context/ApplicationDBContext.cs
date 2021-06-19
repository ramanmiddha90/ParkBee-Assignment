using Microsoft.EntityFrameworkCore;
using ParkBee.Core.Application.Common.Interfaces;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using ParkBee.Core.Domain.GarageAggregate.ValueObjects;
using ParkBee.Infrastructure.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Infrastructure.Context
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext 
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<GarageDetail> GarageDetails { get; set; }
        public  DbSet<Door> Doors { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<GarageDoorStatusHistory> DoorsStatusHistory { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GarageDetailConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressesConfiguration).Assembly);
        }
    }
}
