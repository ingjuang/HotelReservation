using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands
{
    public class SetRoomToHotelCommand : IRequest<PetitionResponse>
    {
        public Guid RoomId { get; set; }
        public Guid HotelId { get; set; }

        public SetRoomToHotelCommand(Guid roomId, Guid hotelId)
        {
            RoomId = roomId;
            HotelId = hotelId;
        }
    }
}
