using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;
using MediatR;

namespace HotelReservation.Application.Commands
{
    public record CreateGuestCommand : IRequest<PetitionResponse>
    {
        public GuestDTO Guest { get; init; }

        public CreateGuestCommand(GuestDTO guest)
        {
            Guest = guest;
        }
    }
}
