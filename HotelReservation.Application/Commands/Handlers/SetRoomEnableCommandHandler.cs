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
    public class SetRoomEnableCommandHandler : IRequestHandler<SetRoomEnableCommand, PetitionResponse>
    {
        private readonly IRoomService _roomService;

        public SetRoomEnableCommandHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<PetitionResponse> Handle(SetRoomEnableCommand request, CancellationToken cancellationToken)
        {
            return await _roomService.SetEnable(request.RoomId, request.IsEnabled);
        }
    }
}
