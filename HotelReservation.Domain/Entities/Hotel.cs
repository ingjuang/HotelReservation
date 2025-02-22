using HotelReservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class Hotel
    {
        public Id Id { get; private set; }
        public HotelName Name { get;  private set; }
        public HotelLocation Location { get; private set; }
        public bool IsEnabled { get; private set; }

        public Hotel(Id id, HotelName name, HotelLocation location, bool isEnabled)
        {
            Id = id;
            Name = name;
            Location = location;
            IsEnabled = isEnabled;
        }
    }

}
