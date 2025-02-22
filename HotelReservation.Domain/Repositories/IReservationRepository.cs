using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task<Reservation> Get(Guid id);
        Task<List<Reservation>> Get();
        Task SetStatus(Guid id, bool status);
    }
}
