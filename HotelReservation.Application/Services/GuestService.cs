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
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public GuestService(IGuestRepository guestRepository, IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _guestRepository = guestRepository;
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }
        public async Task<PetitionResponse> CreateGuest(GuestDTO guestDTO)
        {
            try
            {
                Reservation reservation = await _reservationRepository.Get(guestDTO.ReservationId);
                if(reservation == null)
                {
                    return new PetitionResponse { Success = false, Message = "Reservation not found" };
                }
                Room room = await _roomRepository.Get(reservation.Room.Id);
                if (room == null)
                {
                    return new PetitionResponse { Success = false, Message = "Room not found" };
                }
                List<Guest> guests = await _guestRepository.GetGuestByReservation(reservation.Id);
                if (guests == null)
                {
                    guests = new List<Guest>();
                }
                if (guests.Count >= room.MaxGuests)
                {
                    return new PetitionResponse { Success = false, Message = "Room is full" };
                }
                var guest = new Guest
                (
                    Id.Create(Guid.NewGuid()),
                    GuestFullName.Create(guestDTO.FirstName, guestDTO.LastName),
                    guestDTO.BirthDate,
                    guestDTO.Gender,
                    GuestDocument.Create(guestDTO.DocumentType, guestDTO.DocumentNumber),
                    GuestEmail.Create(guestDTO.Email),
                    guestDTO.Phone,
                    reservation
                );
                guestDTO.Id = guest.Id;
                await _guestRepository.CreateGuest(guest);
                return new PetitionResponse{
                    Success = true, 
                    Message ="Guest created successfully",
                    Result = guestDTO 
                };

            }catch(Exception ex)
            {
                return new PetitionResponse{Success = false, Message = "Error creating guest" };
            }
        }
    }
}
