using HotelReservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class Guest
    {
        public Id Id { get; private set; }
        public GuestFullName FullName { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public string Gender { get; private set; }
        public GuestDocument Document { get; private set; }
        public GuestEmail Email { get; private set; }
        public string Phone { get; private set; }
        public Reservation Reservation { get; private set; }

        public Guest(Id id, GuestFullName fullName, DateOnly birth, string gender, GuestDocument document, GuestEmail email, string phone, Reservation reservation)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birth;
            Gender = gender;
            Document = document;
            Email = email;
            Phone = phone;
            Reservation = reservation;
        }

    }
}
