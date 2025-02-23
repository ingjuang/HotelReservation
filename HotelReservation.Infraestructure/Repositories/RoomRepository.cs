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
    public class RoomRepository : IRoomRepository
    {
        private readonly MongoDBContext _db;
        private readonly IMongoCollection<RoomDTO> collection;
        private readonly IHotelRepository _hotelRepository;
        public RoomRepository(MongoDBContext db, IHotelRepository hotelRepository)
        {
            _db = db;
            collection = _db.GetCollection<RoomDTO>("Rooms");
            _hotelRepository = hotelRepository;
        }
        public async Task<Room> CreateRoom(Room room)
        {
            var roomDto = new RoomDTO
            {
                Id = room.Id.Value,
                HotelId = room.Hotel.Id,
                Price = room.Price.Price,
                Taxes = room.Price.Taxes,
                Type = room.Type,
                IsAvailable = room.IsAvailable,
                IsEnabled = room.IsEnabled,
                Location = room.Location,
                MaxGuests = room.MaxGuests
            };
            await collection.InsertOneAsync(roomDto);
            return room;
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            var roomDto = new RoomDTO
            {
                Id = room.Id.Value,
                HotelId = room.Hotel.Id,
                Price = room.Price.Price,
                Taxes = room.Price.Taxes,
                Type = room.Type,
                IsAvailable = room.IsAvailable,
                IsEnabled = room.IsEnabled,
                Location = room.Location,
                MaxGuests = room.MaxGuests
            };
            await collection.ReplaceOneAsync(r => r.Id == room.Id.Value, roomDto);
            return room;
        }

        public async Task<Room> Get(Guid id)
        {
            RoomDTO roomDto = await collection.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (roomDto == null) return null;
            Hotel hotel = await _hotelRepository.Get(roomDto.HotelId);
            return new Room
            (
                Id.Create(roomDto.Id),
                hotel,
                RoomPrice.Create(roomDto.Price, roomDto.Taxes),
                roomDto.Type,
                roomDto.Location,
                roomDto.MaxGuests,
                roomDto.IsAvailable,
                roomDto.IsEnabled
            );
        }

        public async Task<List<Room>> GetByHotelRooms(Guid hotelId)
        {
            var roomDtos = await collection.Find(r => r.HotelId == hotelId).ToListAsync();
            Hotel hotel = await _hotelRepository.Get(hotelId);
            return roomDtos.Select(r => new Room
            (
                Id.Create(r.Id),
                hotel,
                RoomPrice.Create(r.Price, r.Taxes),
                r.Type,
                r.Location,
                r.MaxGuests,
                r.IsAvailable,
                r.IsEnabled
            )).ToList();
        }

        public async Task<List<Room>> SearchAvalibleRooms(DateRange stayPeriod, int guests, string city)
        {
            List<Hotel> hotels = await _hotelRepository.GetByCity(city);
            var hotelIds = hotels.Select(h => h.Id.Value).ToList();

            var filter = Builders<RoomDTO>.Filter.And(
                Builders<RoomDTO>.Filter.In(r => r.HotelId, hotelIds),
                Builders<RoomDTO>.Filter.Eq(r => r.IsAvailable, true),
                Builders<RoomDTO>.Filter.Eq(r => r.IsEnabled, true),
                Builders<RoomDTO>.Filter.Gte(r => r.MaxGuests, guests)
            );

            List<RoomDTO> roomDtos = await collection.Find(filter).ToListAsync();

            var availableRooms = new List<Room>();
            foreach (var roomDto in roomDtos)
            {
                var hotel = hotels.SingleOrDefault(h => h.Id == roomDto.HotelId);
                availableRooms.Add(new Room(
                    Id.Create(roomDto.Id),
                    hotel,
                    RoomPrice.Create(roomDto.Price, roomDto.Taxes),
                    roomDto.Type,
                    roomDto.Location,
                    roomDto.MaxGuests,
                    roomDto.IsAvailable,
                    roomDto.IsEnabled
                ));
            }

            return availableRooms;
        }

        public async Task SetEnable(Guid id, bool isEnabled)
        {
            var update = Builders<RoomDTO>.Update.Set(r => r.IsEnabled, isEnabled);
            await collection.UpdateOneAsync(r => r.Id == id, update);
        }

        public async Task SetRoomToHotel(Guid roomId, Guid hotelId)
        {
            var update = Builders<RoomDTO>.Update.Set(r => r.HotelId, hotelId);
            await collection.UpdateOneAsync(r => r.Id == roomId, update);
        }
    }
}
