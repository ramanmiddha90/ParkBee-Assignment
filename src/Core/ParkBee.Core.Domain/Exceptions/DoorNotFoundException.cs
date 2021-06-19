using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Core.Domain.Exceptions
{
  public  class DoorNotFoundException : Exception
    {
        public DoorNotFoundException() : base()
        {

        }

        public DoorNotFoundException(string message) : base(message)
        {

        }
    }
}
