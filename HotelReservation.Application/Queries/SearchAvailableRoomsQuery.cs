using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Queries
{
    public class SearchAvailableRoomsQuery : IRequest<PetitionResponse>
    {
        public DateRange StayPeriod { get; set; }
        public int Guests { get; set; }
        public string City { get; set; }

        public SearchAvailableRoomsQuery(DateRange stayPeriod, int guests, string city)
        {
            StayPeriod = stayPeriod;
            Guests = guests;
            City = city;
        }
    }
}
