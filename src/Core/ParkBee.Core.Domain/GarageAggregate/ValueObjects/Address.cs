using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Core.Domain.GarageAggregate.ValueObjects
{
    /// <summary>
    /// Class representing address value object
    /// </summary>
    public class Address : ValueObject
    {

         public Address() { }

        public Guid AddressId { get; private set; }
        /// <summary>
        /// Street address of the garage
        /// </summary>
        public string StreetAddress { get; private set; }

        /// <summary>
        /// The postal code of the garage
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        /// The city in which the garage is located
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// The two letter ISO 3166-1 countrycode of a garage
        /// </summary>
        public string CountryCode { get; private set; }

        /// <summary>
        /// The two letter ISO 3166-1 countrycode of a garage
        /// </summary>
        public string ZoneNumber { get; private set; }

        public Address(Guid addressId, string streetAddress, string postalCode, string city, string countryCode, string zoneNumber)
        {
            this.AddressId = addressId;
            this.StreetAddress = streetAddress;
            this.PostalCode = postalCode;
            this.City = city;
            this.CountryCode = countryCode;
            this.ZoneNumber = zoneNumber;

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StreetAddress;
            yield return PostalCode;
            yield return City;
            yield return CountryCode;
            yield return ZoneNumber;
        }
    }
}
