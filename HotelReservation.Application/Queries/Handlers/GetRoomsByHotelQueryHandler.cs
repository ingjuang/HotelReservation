using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Queries.Handlers
{
    public class GetRoomsByHotelQueryHandler : IRequestHandler<GetRoomsByHotelQuery, PetitionResponse>
    {
        private readonly IRoomService _roomService;

        public GetRoomsByHotelQueryHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<PetitionResponse> Handle(GetRoomsByHotelQuery request, CancellationToken cancellationToken)
        {
            return await _roomService.GetByHotelRooms(request.HotelId);
        }
    }
}
