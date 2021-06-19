using ParkBee.Core.Domain.Common;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using ParkBee.Core.Domain.GarageAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkBee.Core.Domain.GarageAggregate.Entities
{
    /// <summary>
    /// Class represeting garage details info
    /// </summary>
    public class GarageDetail : Entity
    {

        public GarageDetail() { }
        /// <summary>
        /// The garage unique id
        /// </summary>
        public Guid GarageId { get; private set; }

        /// <summary>
        /// The name of the garage, including zone number
        /// </summary>
        public string GarageName { get; private set; }

        /// <summary>
        /// The name of the garage
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Latitude coordinate of the garage
        /// </summary>
        public Int32 Latitide { get; private set; }
        /// <summary>
        /// Latitude coordinate of the garage
        /// </summary>
        public Int32 Longitude { get; private set; }

        /// <summary>
        /// Address of the garage
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// Indicates if there is are entry/exit barriers for this location
        /// </summary>
        public Boolean HasBarrier { get; private set; }

        /// <summary>
        /// Indicates if the garage is suspended or not
        /// </summary>
        public Boolean IsSuspended { get; private set; }

           /// <summary>
        /// Indicates if the garage is suspended or not
        /// </summary>
        public IList<Door> Doors { get; private set; }

        #region Public Methods
        public GarageDetail UpdateDoorStatus(bool status, string IPAddress)
        {
            var door = Doors.FirstOrDefault(x => x.IPAddress == IPAddress);
            //if (door is null)
            //    throw new DomainException("Door is offline or not reachable atm");
            door.UpdateDoorStatus(status);
            return this;
        }

        public GarageDetail(Guid GarageId, string GarageName, string name, Int32 Latitide, Int32 Longitude,Address Address, Boolean HasBarrier, Boolean IsSuspended, IList<Door> Doors)
        {
            this.GarageId = GarageId;
            this.GarageName = GarageName;
            this.GarageName = name;
            this.Latitide = Latitide;
            this.Longitude = Longitude;
            this.Address = Address;
            this.HasBarrier = HasBarrier;
            this.IsSuspended = IsSuspended;
            this.Doors = Doors;
        }
        #endregion

    }
}
