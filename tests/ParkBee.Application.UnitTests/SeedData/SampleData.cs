using ParkBee.Core.Application.Garages.Commands;
using ParkBee.Core.Domain.GarageAggregate.Entities;
using ParkBee.Core.Domain.GarageAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Core.Application.UnitTests.SeedData
{
    public static class SampleData
    {
        public static GarageDetail GetGarageDetails()
        {
            var address1Id = new Guid("b779c9d5-3d5a-49cb-a8b8-899a03737d8b");
            var address2Id = new Guid("18a12e92-624e-4d31-88b2-a02ded9e27f3");

            var garage1 = new Guid("3d4d7fbc-08cc-4d4d-b7da-b88a9eba511d");
            var garage2 = new Guid("42c4bae9-8d5a-4949-a120-a50cc3a0bde1");

            var garage1Door1Id = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f");
            var garage1Door2Id = new Guid("717decdd-69ba-43af-b5b5-94877f6d0ee4");
            var garage1Door3Id = new Guid("f3ba8416-5908-43f7-82b8-d24cca51a348");

            var garage2Door1Id = new Guid("04a6893b-17cc-4ab8-9a0b-5c6ee52d4cfe");
            var garage2Door2Id = new Guid("5ef3c9a4-4940-417f-b1b5-f9b713a02e0d");
            var garage2Door3Id = new Guid("3a3d99d6-a9de-4cf8-b089-590bb79949aa");

            var address1 = new Address(address1Id, "Street 1", "33505", "Maastrich", "NL", "1");
            var address2 = new Address(address2Id, "Street 2", "33416", "Rotterdam", "NL", "2");
            var addresses = new Address[] { address1, address2 };

            var garage1door1 = new Door(garage1Door1Id, "Human", "Human Door", false, "garage1Door1IPId");
            var garage1door2 = new Door(garage1Door2Id, "Entry", "Entry Door", true, "garage1Door2IPId");
            var garage1door3 = new Door(garage1Door3Id, "Exit", "Exit Door", true, "garage1Door3IPId");

            var garage1Doors = new List<Door>() { garage1door1, garage1door2, garage1door3 };

            var garage2door1 = new Door(garage2Door1Id, "Entry", "Human Door", true, "garage2Door1IPId");
            var garage2door2 = new Door(garage2Door2Id, "Entry", "Entry Door", true, "garage2Door2IPId");
            var garage2door3 = new Door(garage2Door3Id, "Exit", "Exit Door", true, "garage2Door3IPId");

            var garage2Doors = new List<Door>() { garage2door1, garage2door2, garage2door3 };

            return new GarageDetail(garage1, "Garage1", "Front", 100, 30, address1, true, false, garage1Doors);
        }

        public static RefreshGarageDoorStatusCommand GetRefreshGarageDoorStatusCommandRequest()
        {
            return new RefreshGarageDoorStatusCommand()
            {
                DoorId = new Guid("5987fe2a-20b4-4d03-aa43-f4c2f5ee2c4f"),
                GargeId = new Guid("3d4d7fbc-08cc-4d4d-b7da-b88a9eba511d"),
                IPAddress = "garage1Door1IPId",
                Status = true
            };
        }

    }

}
