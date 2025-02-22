using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> Get(Guid id);
        Task<Hotel> CreateHotel(Hotel hotel);
        Task<Hotel> UpdateHotel(Hotel hotel);
        Task SetEnable(Guid id, bool isEnabled);
        Task<List<Hotel>> GetByCity(string city);
    }
}
