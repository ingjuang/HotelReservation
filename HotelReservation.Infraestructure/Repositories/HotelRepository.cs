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
    public class HotelRepository : IHotelRepository
    {
        private readonly MongoDBContext _db;
        private readonly IMongoCollection<HotelDTO> collection;
        public HotelRepository(MongoDBContext db)
        {
            _db = db;
            collection = _db.GetCollection<HotelDTO>("Hotels");
        }

        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            HotelDTO hotelDto = new HotelDTO
            {
                Id = hotel.Id,
                Name = hotel.Name.Value,
                Country = hotel.Location.Country,
                City = hotel.Location.City,
                Address = hotel.Location.Address,
                IsEnabled = hotel.IsEnabled
            };
            await collection.InsertOneAsync(hotelDto);
            return hotel;
        
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            HotelDTO hotelDto = new HotelDTO
            {
                Id = hotel.Id,
                Name = hotel.Name.Value,
                Country = hotel.Location.Country,
                City = hotel.Location.City,
                Address = hotel.Location.Address,
                IsEnabled = hotel.IsEnabled
            };
            await collection.ReplaceOneAsync(h => h.Id == hotel.Id, hotelDto);
            return hotel;
        }

        public async Task<Hotel> Get(Guid id)
        {
            HotelDTO hotelDto = await collection.Find(h => h.Id == id).FirstOrDefaultAsync();
            if (hotelDto == null) return null;

            return new Hotel
            (
                Id.Create(hotelDto.Id),
                HotelName.Create(hotelDto.Name),
                HotelLocation.Create(hotelDto.Country, hotelDto.City, hotelDto.Address),
                hotelDto.IsEnabled
            );
        }

        public async Task SetEnable(Guid id, bool isEnabled)
        {
            var update = Builders<HotelDTO>.Update.Set(h => h.IsEnabled, isEnabled);
            var resp = await collection.UpdateOneAsync(h => h.Id == id, update);
            Console.WriteLine(resp);
        }

        public async Task<List<Hotel>> GetByCity(string city)
        {
            List<HotelDTO> hotelDtos = await collection.Find(h => h.City == city).ToListAsync();
            return hotelDtos.Select(h => new Hotel
            (
                Id.Create(h.Id),
                HotelName.Create(h.Name),
                HotelLocation.Create(h.Country, h.City, h.Address),
                h.IsEnabled
            )).ToList();
        }
    }
}
