using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Infraestructure.DBContext;
using HotelReservation.Infraestructure.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infraestructure.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly MongoDBContext _db;
        private readonly IMongoCollection<GuestDTO> collection;
        private readonly IReservationRepository _reservationRepository;

        public GuestRepository(MongoDBContext db, IReservationRepository reservationRepository)
        {
            _db = db;
            collection = _db.GetCollection<GuestDTO>("Guests");
            _reservationRepository = reservationRepository;
        }

        public async Task<Guest> CreateGuest(Guest guest)
        {
            var guestDto = new GuestDTO
            {
                Id = guest.Id,
                FirstName = guest.FullName.FirstName,
                LastName = guest.FullName.LastName,
                BirthDate = guest.BirthDate,
                Gender = guest.Gender,
                DocumentType = guest.Document.DocumentType,
                DocumentNumber = guest.Document.DocumentNumber,
                Email = guest.Email.Value,
                Phone = guest.Phone,
                ReservationId = guest.Reservation.Id
            };
            await collection.InsertOneAsync(guestDto);
            return guest;
        }

        public async Task<List<Guest>> GetGuestByReservation(Guid reservationId)
        {
            List<GuestDTO> guests = await collection.Find(g => g.ReservationId == reservationId).ToListAsync();
            Reservation reservation = await _reservationRepository.Get(reservationId);
            return guests.Select(g => new Guest
            (
                Id.Create(g.Id),
                GuestFullName.Create(g.FirstName, g.LastName),
                g.BirthDate,
                g.Gender,
                GuestDocument.Create(g.DocumentType, g.DocumentNumber),
                GuestEmail.Create(g.Email),
                g.Phone,
                reservation
                )).ToList();
        }

    }
}
