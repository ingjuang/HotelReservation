using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Repositories
{
    public interface IGuestRepository
    {
        Task<Guest> CreateGuest(Guest guest);
        Task<List<Guest>> GetGuestByReservation(Guid reservationId);
    }
}
