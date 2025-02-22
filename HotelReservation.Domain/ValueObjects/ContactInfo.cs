using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record ContactInfo
    {
        public String FullName { get; }
        public String PhoneNumber { get; }

        protected ContactInfo(String fullName, String phoneNumber)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public static ContactInfo Create(String fullName, String phoneNumber)
        {
            Validate(fullName, phoneNumber);
            return new ContactInfo(fullName, phoneNumber);
        }

        private static void Validate(String fullName, String phoneNumber)
        {
            if (String.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Full name cannot be empty");
            }
            if (String.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be empty");
            }
        }
    }
}
