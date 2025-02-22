using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record GuestEmail
    {
        public string Value { get; }
        internal GuestEmail(string value)
        {
            Value = value;
        }
        public static GuestEmail Create(string value)
        {
            Validate(value);
            return new GuestEmail(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email cannot be empty");
            }
            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(emailPattern, value))
            {
                throw new ArgumentException("Email is invalid");
            }
        }
    }
}
