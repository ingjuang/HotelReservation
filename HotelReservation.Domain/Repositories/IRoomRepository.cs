using HotelReservation.Domain.Entities;
using HotelReservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateRoom(Room room);
        Task<Room> UpdateRoom(Room room);
        Task<Room> Get(Guid id);
        Task<List<Room>> GetByHotelRooms(Guid hotelId);
        Task<List<Room>> SearchAvalibleRooms(DateRange stayPeriod, int guests, string city);
        Task SetEnable(Guid id, bool isEnabled);
        Task SetRoomToHotel(Guid roomId, Guid hotelId);
    }
}
