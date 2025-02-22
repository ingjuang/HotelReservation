using HotelReservation.Application.DTOs;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;

namespace HotelReservation.Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<PetitionResponse> CreateRoom(RoomDTO roomDTO);
        Task<PetitionResponse> GetByHotelRooms(Guid hotelId);
        Task<PetitionResponse> SearchAvalibleRooms(DateRange stayPeriod, int guests, string city);
        Task<PetitionResponse> SetEnable(Guid id, bool isEnabled);
        Task<PetitionResponse> SetRoomToHotel(Guid roomId, Guid hotelId);
    }
}
