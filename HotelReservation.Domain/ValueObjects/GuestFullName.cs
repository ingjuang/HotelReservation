using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record GuestFullName
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        protected GuestFullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static GuestFullName Create(string firstName, string lastName)
        {
            Validate(firstName, lastName);
            return new GuestFullName(firstName, lastName);
        }

        private static void Validate(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty");
            }

        }
    }
}
