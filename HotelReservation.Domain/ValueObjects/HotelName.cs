using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record HotelName
    {
        public string Value { get; }
        internal HotelName(string value)
        {
            Value = value;
        }

        public static HotelName Create(string value)
        {
            Validate(value);
            return new HotelName(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Hotel name cannot be empty");
            }
        }
    }
}
