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
    public class ReservationRepository : IReservationRepository
    {
        private readonly MongoDBContext _db;
        private readonly IMongoCollection<ReservationDTO> collection;
        private readonly IRoomRepository _roomRepository;

        public ReservationRepository(MongoDBContext db, IRoomRepository roomRepository)
        {
            _db = db;
            collection = _db.GetCollection<ReservationDTO>("Reservations");
            _roomRepository = roomRepository;
        }
        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            var reservationDto = new ReservationDTO
            {
                Id = reservation.Id,
                RoomId = reservation.Room.Id,
                StayPeriodStart = reservation.StayPeriod.StartDate.ToDateTime(TimeOnly.MinValue),
                StayPeriodEnd = reservation.StayPeriod.EndDate.ToDateTime(TimeOnly.MinValue),
                Status = reservation.Status,
                EmergencyContactFullName = reservation.EmergencyContact.FullName,
                EmergencyContactPhone = reservation.EmergencyContact.PhoneNumber
            };
            await collection.InsertOneAsync(reservationDto);
            return reservation;
        }

        public async Task<Reservation> Get(Guid id)
        {
            var reservationDto = await collection.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (reservationDto == null) return null;
            Room room = await _roomRepository.Get(reservationDto.RoomId);
            return new Reservation
                (
                    Id.Create(reservationDto.Id),
                    room,
                    DateRange.Create(DateOnly.FromDateTime(reservationDto.StayPeriodStart), DateOnly.FromDateTime(reservationDto.StayPeriodEnd)),
                    ContactInfo.Create(reservationDto.EmergencyContactFullName, reservationDto.EmergencyContactPhone),
                    reservationDto.Status
                );
        }

        public async Task<List<Reservation>> Get()
        {
            var reservations = await collection.Find(r => true).ToListAsync();
            List<Reservation> reservationsList = new List<Reservation>();
            foreach (var reservation in reservations)
            {
                Room room = await _roomRepository.Get(reservation.RoomId);
                reservationsList.Add(new Reservation
                    (
                        Id.Create(reservation.Id),
                        room,
                        DateRange.Create(DateOnly.FromDateTime(reservation.StayPeriodStart), DateOnly.FromDateTime(reservation.StayPeriodEnd)),
                        ContactInfo.Create(reservation.EmergencyContactFullName, reservation.EmergencyContactPhone),
                        reservation.Status
                    ));
            }
            return reservationsList;
        }

        public async Task SetStatus(Guid id, bool status)
        {
            var update = Builders<ReservationDTO>.Update.Set(r => r.Status, status);
            await collection.UpdateOneAsync(r => r.Id == id, update);
        }
    }
}
