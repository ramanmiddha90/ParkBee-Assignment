using System;

namespace ParkBee.Core.Domain.Exceptions
{
    public class DoorNotReachableException : Exception
    {
        public DoorNotReachableException() : base()
        {

        }

        public DoorNotReachableException(string message): base(message)
        {

        }
    }
}
