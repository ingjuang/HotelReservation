using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;
using MediatR;

namespace HotelReservation.Application.Commands.Handlers
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, PetitionResponse>
    {
        private readonly IHotelService _hotelService;

        public CreateHotelCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<PetitionResponse> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotelDTO = request.HotelDTO;
            PetitionResponse response = await _hotelService.CreateHotel(hotelDTO);
            return response;
        }
    }
}
