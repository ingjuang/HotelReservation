using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands
{
    public class CreateRoomCommand : IRequest<PetitionResponse>
    {
        public RoomDTO Room { get; set; }

        public CreateRoomCommand(RoomDTO room)
        {
            Room = room;
        }
    }
}
