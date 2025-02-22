using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;

namespace HotelReservation.Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<PetitionResponse> CreateReservation(ReservationDTO reservationDTO);
        Task<PetitionResponse> Get();
        Task<PetitionResponse> Get(Guid id);
        Task<PetitionResponse> SetStatus(Guid id, bool status);
    }
}
