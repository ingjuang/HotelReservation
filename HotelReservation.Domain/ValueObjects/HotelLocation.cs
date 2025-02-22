using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record HotelLocation
    {
        public string Country { get; init; }
        public string City { get; init; }
        public string Address { get; init; }

        internal HotelLocation(string country, string city, string address)
        {
            Country = country;
            City = city;
            Address = address;
        }

        public static HotelLocation Create(string country, string city, string address)
        {
            Validate(country, city, address);
            return new HotelLocation(country, city, address);
        }

        private static void Validate(string country, string city, string address)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("Country cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address cannot be empty");
            }

        }
    }
}
