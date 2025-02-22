using HotelReservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class Reservation
    {
        public Id Id { get; private     set; }
        public Room Room { get; private set; }
        public DateRange StayPeriod { get; private set; }
        public bool Status { get; private set; }
        public ContactInfo EmergencyContact { get; private set; }

        public Reservation(Id id, Room room, DateRange stayPeriod, ContactInfo emergencyContact, bool status)
        {
            Id = id;
            Room = room;
            StayPeriod = stayPeriod;
            EmergencyContact = emergencyContact;
            Status = status;
        }
    }
}
