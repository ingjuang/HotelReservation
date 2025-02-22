using HotelReservation.Application.DTOs;
using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;

namespace HotelReservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IGuestRepository _guestRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository, IGuestRepository guestRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _guestRepository = guestRepository;
        }
        public async Task<PetitionResponse> CreateReservation(ReservationDTO reservationDTO)
        {
            try
            {
                Room room = await _roomRepository.Get(reservationDTO.RoomId);
                if (room == null) {
                    return new PetitionResponse { Success = false, Message = "Room not found" };
                }
                if (!room.IsAvailable)
                {
                    return new PetitionResponse { Success = false, Message = "Room is not available" };
                }
                if (!room.IsEnabled)
                {
                    return new PetitionResponse { Success = false, Message = "Room is not enabled" };
                }
                if (reservationDTO.Guests.Count > room.MaxGuests)
                {
                    return new PetitionResponse { Success = false, Message = "Room capacity exceeded" };
                }
                Reservation reservation = new Reservation
                (
                    Id.Create(Guid.NewGuid()),
                    room,
                    DateRange.Create(reservationDTO.StartDate, reservationDTO.EndDate),
                    ContactInfo.Create(reservationDTO.EmergencyContactFullName, reservationDTO.EmergencyContactPhone),
                    reservationDTO.Status
                );
                foreach (var guest in reservationDTO.Guests)
                {
                    Guest newGuest = new Guest
                    (
                        Id.Create(Guid.NewGuid()),
                        GuestFullName.Create(guest.FirstName, guest.LastName),
                        guest.BirthDate,
                        guest.Gender,
                        GuestDocument.Create(guest.DocumentType, guest.DocumentNumber),
                        GuestEmail.Create(guest.Email),
                        guest.Phone,
                        reservation
                        );
                    guest.Id = newGuest.Id;
                    await _guestRepository.CreateGuest(newGuest);
                }
                await _reservationRepository.CreateReservation(reservation);
                reservationDTO.Id = reservation.Id;
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Reservation created successfully",
                    Result = reservationDTO
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error creating reservation: {ex.Message}" };
            }
        }

        public async Task<PetitionResponse> Get()
        {
            try
            {
                // Obtener todas las reservas
                List<Reservation> reservations = await _reservationRepository.Get();
                if (reservations == null || !reservations.Any())
                {
                    return new PetitionResponse
                    {
                        Success = true,
                        Message = "No reservations found",
                        Result = new List<ReservationDTO>()
                    };
                }

                // Obtener todos los huéspedes en paralelo para cada reserva
                var guestTasks = reservations.Select(res => _guestRepository.GetGuestByReservation(res.Id));
                var guestsList = await Task.WhenAll(guestTasks);

                // Construir la lista de DTOs
                List<ReservationDTO> response = reservations.Select((res, index) => new ReservationDTO
                {
                    Id = res.Id.Value,
                    RoomId = res.Room.Id.Value,
                    GuestIds = guestsList[index].Select(g => g.Id.Value.ToString()).ToList(),
                    StartDate = res.StayPeriod.StartDate,
                    EndDate = res.StayPeriod.EndDate,
                    Status = res.Status,
                    EmergencyContactFullName = res.EmergencyContact.FullName,
                    EmergencyContactPhone = res.EmergencyContact.PhoneNumber,
                    Guests = guestsList[index].Select(g => new GuestDTO
                    {
                        FirstName = g.FullName.FirstName,
                        LastName = g.FullName.LastName,
                        BirthDate = g.BirthDate,
                        Gender = g.Gender,
                        DocumentType = g.Document.DocumentType,
                        DocumentNumber = g.Document.DocumentNumber,
                        Email = g.Email.Value,
                        Phone = g.Phone,
                        ReservationId = g.Reservation.Id.Value
                    }).ToList()
                }).ToList();

                return new PetitionResponse
                {
                    Success = true,
                    Message = "Reservations obtained successfully",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Message = $"Error getting reservations: {ex.Message}"
                };
            }
        }

        public async Task<PetitionResponse> Get(Guid id)
        {
            try
            {
                Reservation reservation = await _reservationRepository.Get(id);
                if (reservation == null)
                {
                    return new PetitionResponse { Success = false, Message = "Reservation not found" };
                }
                List<Guest> guests = await _guestRepository.GetGuestByReservation(id);
                ReservationDTO response = new ReservationDTO
                {
                    Id = reservation.Id.Value,
                    RoomId = reservation.Room.Id.Value,
                    GuestIds = guests.Select(g => g.Id.Value.ToString()).ToList(),
                    StartDate = reservation.StayPeriod.StartDate,
                    EndDate = reservation.StayPeriod.EndDate,
                    Status = reservation.Status,
                    EmergencyContactFullName = reservation.EmergencyContact.FullName,
                    EmergencyContactPhone = reservation.EmergencyContact.PhoneNumber,
                    Guests = guests.Select(g => new GuestDTO
                    {
                        FirstName = g.FullName.FirstName,
                        LastName = g.FullName.LastName,
                        BirthDate = g.BirthDate,
                        Gender = g.Gender,
                        DocumentType = g.Document.DocumentType,
                        DocumentNumber = g.Document.DocumentNumber,
                        Email = g.Email.Value,
                        Phone = g.Phone,
                        ReservationId = g.Reservation.Id.Value
                    }).ToList()
                };
                return new PetitionResponse
                {
                    Success = true,
                    Message = "Reservation obtained successfully",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error getting reservation: {ex.Message}" };
            }
        }


        public async Task<PetitionResponse> SetStatus(Guid id, bool status)
        {
            try
            {
                await _reservationRepository.SetStatus(id, status);
                return new PetitionResponse { Success = true, Message = "Status set successfully" };
            }
            catch (Exception ex)
            {
                return new PetitionResponse { Success = false, Message = $"Error setting status: {ex.Message}" };
            }
        }
    }
}
