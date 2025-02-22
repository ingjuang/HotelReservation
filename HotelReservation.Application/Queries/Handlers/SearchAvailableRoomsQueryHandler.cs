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
    public class SearchAvailableRoomsQueryHandler : IRequestHandler<SearchAvailableRoomsQuery, PetitionResponse>
    {
        private readonly IRoomService _roomService;

        public SearchAvailableRoomsQueryHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<PetitionResponse> Handle(SearchAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _roomService.SearchAvalibleRooms(request.StayPeriod, request.Guests, request.City);
        }
    }
}
