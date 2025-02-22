using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Queries
{
    public class GetRoomsByHotelQuery : IRequest<PetitionResponse>
    {
        public Guid HotelId { get; set; }

        public GetRoomsByHotelQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }
    }
}
