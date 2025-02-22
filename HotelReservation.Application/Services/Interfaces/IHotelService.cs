using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;

namespace HotelReservation.Application.Services.Interfaces
{
    public interface IHotelService
    {
        Task<PetitionResponse> CreateHotel(HotelDTO hotelDTO);
        Task<PetitionResponse> SetEnable(Guid id, bool isEnabled);
        Task<PetitionResponse> UpdateHotel(HotelDTO hotelDTO);
        Task<PetitionResponse> GetHotelByCity(string city);
    }
}
