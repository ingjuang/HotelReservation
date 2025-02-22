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
    public class GetHotelByCityQueryHandler : IRequestHandler<GetHotelByCityQuery, PetitionResponse>
    {
        private readonly IHotelService _hotelService;

        public GetHotelByCityQueryHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<PetitionResponse> Handle(GetHotelByCityQuery request, CancellationToken cancellationToken)
        {
            return await _hotelService.GetHotelByCity(request.City);
        }
    }
}
