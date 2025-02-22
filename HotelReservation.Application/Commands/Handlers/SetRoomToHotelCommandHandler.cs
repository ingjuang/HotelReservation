using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands.Handlers
{
    public class SetRoomToHotelCommandHandler : IRequestHandler<SetRoomToHotelCommand, PetitionResponse>
    {
        private readonly IRoomService _roomService;

        public SetRoomToHotelCommandHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<PetitionResponse> Handle(SetRoomToHotelCommand request, CancellationToken cancellationToken)
        {
            return await _roomService.SetRoomToHotel(request.RoomId, request.HotelId);
        }
    }
}
