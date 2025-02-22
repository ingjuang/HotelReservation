using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands
{
    public class SetRoomEnableCommand : IRequest<PetitionResponse>
    {
        public Guid RoomId { get; set; }
        public bool IsEnabled { get; set; }

        public SetRoomEnableCommand(Guid roomId, bool isEnabled)
        {
            RoomId = roomId;
            IsEnabled = isEnabled;
        }
    }
}
