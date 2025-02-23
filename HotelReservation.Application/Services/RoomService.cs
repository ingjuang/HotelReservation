using HotelReservation.Application.DTOs;
using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Infraestructure.Repositories;
using HotelReservation.Util.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IReservationRepository _reservationRepository;
        public RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository, IReservationRepository reservationRepository)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _reservationRepository = reservationRepository;
        }
        public async Task<PetitionResponse> CreateRoom(RoomDTO roomDTO)
        {
            try
            {
                Hotel hotel = await _hotelRepository.Get(roomDTO.HotelId);
                if(hotel  == null) {
                    return new PetitionResponse { Success = false, Message = "Hotel not found" };
                }
                Room room = new Room
                (
                    Id.Create(Guid.NewGuid()),
                    hotel,
                    RoomPrice.Create(roomDTO.Price, roomDTO.Taxes),
                    roomDTO.Type,
                    roomDTO.Location,
                    roomDTO.MaxGuests,
                    roomDTO.IsAvailable,
                    roomDTO.IsEnabled
                );
                await _roomRepository.CreateRoom(room);
                roomDTO.Id = room.Id;
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Room created successfully",
                    Result = roomDTO
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error creating room: {ex.Message}" };
            }
        }

        public async Task<PetitionResponse> GetByHotelRooms(Guid hotelId)
        {
            try
            {
                List<Room> room = await _roomRepository.GetByHotelRooms(hotelId);
                if (room.Count == 0)
                {
                    return new PetitionResponse { Success = false, Message = "Rooms not found" };
                }
                List<RoomDTO> response = new List<RoomDTO>();
                foreach (var item in room)
                {
                    response.Add(new RoomDTO
                    {
                        Id = item.Id.Value,
                        HotelId = item.Hotel.Id.Value,
                        Price = item.Price.Price,
                        Taxes = item.Price.Taxes,
                        Type = item.Type,
                        IsAvailable = item.IsAvailable,
                        IsEnabled = item.IsEnabled,
                        Location = item.Location,
                        MaxGuests = item.MaxGuests
                    });
                }
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Rooms found",
                    Result = response
                };

            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error getting rooms: {ex.Message}" };
            }
        }

        public async Task<PetitionResponse> SearchAvalibleRooms(DateRange stayPeriod, int guests, string city)
        {
            try
            {
                List<Room> rooms = await _roomRepository.SearchAvalibleRooms(stayPeriod, guests, city);
                if (rooms.Count == 0)
                {
                    return new PetitionResponse { Success = false, Message = "Rooms not found" };
                }
                List<RoomDTO> response = new List<RoomDTO>();
                foreach (Room room in rooms)
                {
                    bool hasActiveReservation = await _reservationRepository.HasActiveReservation(room.Id, stayPeriod);
                    if (!hasActiveReservation)
                    {
                        response.Add(new RoomDTO
                        {
                            Id = room.Id.Value,
                            HotelId = room.Hotel.Id.Value,
                            Price = room.Price.Price,
                            Taxes = room.Price.Taxes,
                            Type = room.Type,
                            IsAvailable = room.IsAvailable,
                            IsEnabled = room.IsEnabled,
                            Location = room.Location,
                            MaxGuests = room.MaxGuests
                        });
                    }
                }
                ;
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Rooms found",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error getting rooms: {ex.Message}" };
            }
        }

        public async Task<PetitionResponse> SetEnable(Guid id, bool isEnabled)
        {
            try
            {
                await _roomRepository.SetEnable(id, isEnabled);
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Room updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error updating room: {ex.Message}" };
            }
        }
        public async Task<PetitionResponse> SetRoomToHotel(Guid roomId, Guid hotelId)
        {
            try
            {
                Room room = await _roomRepository.Get(roomId);
                if (room == null)
                {
                    return new PetitionResponse { Success = false, Message = "Room not found" };
                }
                Hotel hotel = await _hotelRepository.Get(hotelId);
                if (hotel == null)
                {
                    return new PetitionResponse { Success = false, Message = "Hotel not found" };
                }
                await _roomRepository.SetRoomToHotel(roomId, hotelId);
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Room set to hotel successfully"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error setting room to hotel: {ex.Message}" };
            }
        }
    }
}
