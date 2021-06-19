using ParkBee.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Core.Domain.GarageAggregate.Entities
{
    public class GarageDoorStatusHistory : Entity
    {
        public GarageDoorStatusHistory() { }

        public Guid Id { get; set; }

        /// <summary>
        ///Unique  Door Id
        /// </summary>
        public Guid DoorId { get; private set; }

        /// <summary>
        ///Door current status
        /// </summary>
        public bool currentStatus { get; private set; }

        /// <summary>
        /// Garage door status
        /// </summary>
        public bool LastStatus { get; private set; }

        /// <summary>
        /// Status modified date
        /// </summary>
        public DateTime ModifiedDate { get; private set; }

        public GarageDoorStatusHistory(Guid doorId, bool currentStatus, bool lastStatus, DateTime modifiedDate)
        {
            this.DoorId = doorId;
            this.currentStatus = currentStatus;
            this.LastStatus = lastStatus;
            this.ModifiedDate = modifiedDate;
        }

    }
}
