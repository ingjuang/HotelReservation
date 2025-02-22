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
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, PetitionResponse>
    {
        private readonly IRoomService _roomService;

        public CreateRoomCommandHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<PetitionResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            return await _roomService.CreateRoom(request.Room);
        }
    }
}
