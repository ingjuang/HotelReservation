using HotelReservation.Application.DTOs;
using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;
using MediatR;
namespace HotelReservation.Application.Commands.Handlers
{
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, PetitionResponse>
    {
        private readonly IGuestService _guestService;

        public CreateGuestCommandHandler(IGuestService guestService)
        {
            _guestService = guestService;
        }

        public async Task<PetitionResponse> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestDTO = request.Guest;
            PetitionResponse response = await _guestService.CreateGuest(guestDTO);
            return response;
        }
    }
}
