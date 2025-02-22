using HotelReservation.Application.DTOs;
using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;

namespace HotelReservation.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<PetitionResponse> CreateHotel(HotelDTO hotelDTO)
        {
            try
            {
                Hotel hotel = new Hotel
                (
                    Id.Create(Guid.NewGuid()),
                    HotelName.Create(hotelDTO.Name),
                    HotelLocation.Create(hotelDTO.Country, hotelDTO.City, hotelDTO.Address),
                    hotelDTO.IsEnabled
                );
                await _hotelRepository.CreateHotel(hotel);
                hotelDTO.Id = hotel.Id;
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Hotel created successfully",
                    Result = hotelDTO
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error creating hotel: {ex.Message}" };
            }
        }

        public async Task<PetitionResponse> SetEnable(Guid id, bool isEnabled)
        {
            try
            {
                Hotel hotel = await _hotelRepository.Get(id);
                if(hotel == null)
                {
                    return new PetitionResponse { Success = false, Message = "hotel not found" };
                }
                await _hotelRepository.SetEnable(id, isEnabled);
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Hotel updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse {Success = false, Message = $"Error updating hotel: {ex.Message}" };
            }
        }

        public async Task<PetitionResponse> UpdateHotel(HotelDTO hotelDTO)
        {
            try
            {
                Hotel hotel = new Hotel
                (
                    Id.Create(hotelDTO.Id),
                    HotelName.Create(hotelDTO.Name),
                    HotelLocation.Create(hotelDTO.Country, hotelDTO.City, hotelDTO.Address),
                    hotelDTO.IsEnabled
                );
                Hotel hotelUpdated = await _hotelRepository.UpdateHotel(hotel);
                HotelDTO response = new HotelDTO
                {
                    Id = hotelUpdated.Id,
                    Name = hotelUpdated.Name.Value,
                    Country = hotelUpdated.Location.Country,
                    City = hotelUpdated.Location.City,
                    Address = hotelUpdated.Location.Address,
                    IsEnabled = hotelUpdated.IsEnabled
                };
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Hotel updated successfully",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error updating hotel: ${ex.Message}" };
            }
        }

        public async Task<PetitionResponse> GetHotelByCity(string city)
        {
            try
            {
                List<Hotel> hotels = await _hotelRepository.GetByCity(city);
                if(hotels == null || hotels.Count == 0)
                {
                    return new PetitionResponse { Success = false, Message = "No hotels found" };
                }
                List<HotelDTO> response = hotels.Select(hotel => new HotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name.Value,
                    Country = hotel.Location.Country,
                    City = hotel.Location.City,
                    Address = hotel.Location.Address,
                    IsEnabled = hotel.IsEnabled
                }).ToList();

                return new PetitionResponse
                {
                    Success = true,
                    Message = "Hotels found",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error getting hotels: {ex.Message}" };
            }
        }
    }
}
