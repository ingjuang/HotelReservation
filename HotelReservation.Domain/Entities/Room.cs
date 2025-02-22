using HotelReservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class Room
    {
        public Id Id { get; private set; }
        public Hotel Hotel { get; private set; }
        public RoomPrice Price { get; private set; }
        public string Type { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsEnabled { get; private set; }
        public string Location { get; private set; }
        public int MaxGuests { get; private set; }
        public Room(Id id, Hotel hotel, RoomPrice price, string type, string location, int maxGuests, bool isAvailable, bool isEnabled)
        {
            Id = id;
            Hotel = hotel;
            Price = price;
            Type = type;
            Location = location;
            IsAvailable = isAvailable;
            IsEnabled = isEnabled;
            MaxGuests = maxGuests;
        }
    }
}
