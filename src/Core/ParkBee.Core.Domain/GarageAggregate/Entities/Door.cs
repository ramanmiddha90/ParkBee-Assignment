using ParkBee.Core.Domain.Common;
using System;

namespace ParkBee.Core.Domain.GarageAggregate.Entities
{
    /// <summary>
    /// class represeting door information
    /// </summary>
    public class Door : Entity
    {
        /// <summary>
        /// The type of the door, this can be Human, Entry or Exit  
        /// </summary>
        public string DoorType { get; private set; }

        /// <summary>
        /// Optional description for a door in case of multiple doors of the same type 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        ///  The unique identifier of a door
        /// </summary>
        public Guid DoorId { get; private set; }

        /// <summary>
        ///  Indicated if door is active or not
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        ///  IPAddress of door
        /// </summary>
        public string IPAddress { get; private set; }

        /// <summary>
        /// update door status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public Door UpdateDoorStatus(bool status)
        {
            IsActive = status;
            return this;
        }
        public Door() { }
        public Door(Guid DoorId,string doorType, string description, bool isActive, string ipAddress)
        {
            this.DoorId = DoorId;
            this.DoorType = doorType;
            this.Description = description;
            this.DoorId = DoorId;
            this.IsActive = isActive;
            this.IPAddress = ipAddress;
        }

    }
}
