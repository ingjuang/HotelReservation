using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;

namespace HotelReservation.Application.Services.Interfaces
{
    public interface IGuestService
    {
        Task<PetitionResponse> CreateGuest(GuestDTO guestDTO);
    }
}
