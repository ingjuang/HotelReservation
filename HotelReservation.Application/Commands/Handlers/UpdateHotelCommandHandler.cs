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
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, PetitionResponse>
    {
        private readonly IHotelService _hotelService;

        public UpdateHotelCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<PetitionResponse> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            return await _hotelService.UpdateHotel(request.HotelDTO);
        }
    }
}
