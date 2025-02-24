using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservation.Application.DTOs;
using HotelReservation.Application.Services;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.ValueObjects;
using HotelReservation.Util.Reponses;
using Moq;
using Xunit;

namespace HotelReservation.Tests.Application.Services
{
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IGuestRepository> _guestRepositoryMock;
        private readonly ReservationService _reservationService;

        public ReservationServiceTests()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _guestRepositoryMock = new Mock<IGuestRepository>();
            _reservationService = new ReservationService(_reservationRepositoryMock.Object, _roomRepositoryMock.Object, _guestRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnSuccessResponse_WhenReservationIsCreated()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                RoomId = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Status = true,
                EmergencyContactFullName = "Emergency Contact",
                EmergencyContactPhone = "1234567890",
                Guests = new List<GuestDTO>
                {
                    new GuestDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                        Gender = "Male",
                        DocumentType = "Passport",
                        DocumentNumber = "123456789",
                        Email = "john.doe@example.com",
                        Phone = "1234567890"
                    }
                }
            };

            var room = new Room(
                Id.Create(reservationDTO.RoomId),
                new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2,
                true,
                true
            );

            var reservation = new Reservation(
                Id.Create(Guid.NewGuid()),
                room,
                DateRange.Create(reservationDTO.StartDate, reservationDTO.EndDate),
                ContactInfo.Create(reservationDTO.EmergencyContactFullName, reservationDTO.EmergencyContactPhone),
                reservationDTO.Status
            );

            var guest = new Guest(
                Id.Create(Guid.NewGuid()),
                GuestFullName.Create(reservationDTO.Guests[0].FirstName, reservationDTO.Guests[0].LastName),
                reservationDTO.Guests[0].BirthDate,
                reservationDTO.Guests[0].Gender,
                GuestDocument.Create(reservationDTO.Guests[0].DocumentType, reservationDTO.Guests[0].DocumentNumber),
                GuestEmail.Create(reservationDTO.Guests[0].Email),
                reservationDTO.Guests[0].Phone,
                reservation
            );

            _roomRepositoryMock.Setup(repo => repo.Get(reservationDTO.RoomId)).ReturnsAsync(room);
            _reservationRepositoryMock.Setup(repo => repo.CreateReservation(It.IsAny<Reservation>())).ReturnsAsync(reservation);
            _guestRepositoryMock.Setup(repo => repo.CreateGuest(It.IsAny<Guest>())).ReturnsAsync(guest);

            // Act
            var result = await _reservationService.CreateReservation(reservationDTO);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Reservation created successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnErrorResponse_WhenRoomNotFound()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                RoomId = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Status = true,
                EmergencyContactFullName = "Emergency Contact",
                EmergencyContactPhone = "1234567890",
                Guests = new List<GuestDTO>
                {
                    new GuestDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                        Gender = "Male",
                        DocumentType = "Passport",
                        DocumentNumber = "123456789",
                        Email = "john.doe@example.com",
                        Phone = "1234567890"
                    }
                }
            };

            _roomRepositoryMock.Setup(repo => repo.Get(reservationDTO.RoomId)).ReturnsAsync((Room)null);

            // Act
            var result = await _reservationService.CreateReservation(reservationDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Room not found", result.Message);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnErrorResponse_WhenRoomIsNotAvailable()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                RoomId = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Status = true,
                EmergencyContactFullName = "Emergency Contact",
                EmergencyContactPhone = "1234567890",
                Guests = new List<GuestDTO>
                {
                    new GuestDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                        Gender = "Male",
                        DocumentType = "Passport",
                        DocumentNumber = "123456789",
                        Email = "john.doe@example.com",
                        Phone = "1234567890"
                    }
                }
            };

            var room = new Room(
                Id.Create(reservationDTO.RoomId),
                new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2,
                false, // Room is not available
                true
            );

            _roomRepositoryMock.Setup(repo => repo.Get(reservationDTO.RoomId)).ReturnsAsync(room);

            // Act
            var result = await _reservationService.CreateReservation(reservationDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Room is not available", result.Message);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnErrorResponse_WhenRoomIsNotEnabled()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                RoomId = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Status = true,
                EmergencyContactFullName = "Emergency Contact",
                EmergencyContactPhone = "1234567890",
                Guests = new List<GuestDTO>
                {
                    new GuestDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                        Gender = "Male",
                        DocumentType = "Passport",
                        DocumentNumber = "123456789",
                        Email = "john.doe@example.com",
                        Phone = "1234567890"
                    }
                }
            };

            var room = new Room(
                Id.Create(reservationDTO.RoomId),
                new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2,
                true,
                false // Room is not enabled
            );

            _roomRepositoryMock.Setup(repo => repo.Get(reservationDTO.RoomId)).ReturnsAsync(room);

            // Act
            var result = await _reservationService.CreateReservation(reservationDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Room is not enabled", result.Message);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnErrorResponse_WhenRoomCapacityExceeded()
        {
            // Arrange
            var reservationDTO = new ReservationDTO
            {
                RoomId = Guid.NewGuid(),
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Status = true,
                EmergencyContactFullName = "Emergency Contact",
                EmergencyContactPhone = "1234567890",
                Guests = new List<GuestDTO>
                {
                    new GuestDTO
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                        Gender = "Male",
                        DocumentType = "Passport",
                        DocumentNumber = "123456789",
                        Email = "john.doe@example.com",
                        Phone = "1234567890"
                    },
                    new GuestDTO
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
                        Gender = "Female",
                        DocumentType = "Passport",
                        DocumentNumber = "987654321",
                        Email = "jane.doe@example.com",
                        Phone = "0987654321"
                    },
                    new GuestDTO
                    {
                        FirstName = "Jack",
                        LastName = "Doe",
                        BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                        Gender = "Male",
                        DocumentType = "Passport",
                        DocumentNumber = "123456789",
                        Email = "jack.doe@example.com",
                        Phone = "1234567890"
                    }
                }
            };

            var room = new Room(
                Id.Create(reservationDTO.RoomId),
                new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2, // Room capacity is 2
                true,
                true
            );

            _roomRepositoryMock.Setup(repo => repo.Get(reservationDTO.RoomId)).ReturnsAsync(room);

            // Act
            var result = await _reservationService.CreateReservation(reservationDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Room capacity exceeded", result.Message);
        }

        [Fact]
        public async Task Get_ShouldReturnSuccessResponse_WhenReservationsAreFound()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new Reservation(
                    Id.Create(Guid.NewGuid()),
                    new Room(Id.Create(Guid.NewGuid()), new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true), RoomPrice.Create(100, 10), "Single", "First Floor", 2, true, true),
                    DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                    ContactInfo.Create("Emergency Contact", "1234567890"),
                    true
                )
            };

            var guests = new List<Guest>
            {
                new Guest(Id.Create(Guid.NewGuid()), GuestFullName.Create("John", "Doe"), DateOnly.FromDateTime(DateTime.Now.AddYears(-30)), "Male", GuestDocument.Create("Passport", "123456789"), GuestEmail.Create("john.doe@example.com"), "1234567890", reservations[0])
            };

            _reservationRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(reservations);
            _guestRepositoryMock.Setup(repo => repo.GetGuestByReservation(It.IsAny<Guid>())).ReturnsAsync(guests);

            // Act
            var result = await _reservationService.Get();

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Reservations obtained successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Get_ShouldReturnSuccessResponse_WhenNoReservationsFound()
        {
            // Arrange
            _reservationRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(new List<Reservation>());

            // Act
            var result = await _reservationService.Get();

            // Assert
            Assert.True(result.Success);
            Assert.Equal("No reservations found", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Get_ShouldReturnSuccessResponse_WhenReservationIsFound()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var reservation = new Reservation(
                Id.Create(reservationId),
                new Room(Id.Create(Guid.NewGuid()), new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true), RoomPrice.Create(100, 10), "Single", "First Floor", 2, true, true),
                DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                ContactInfo.Create("Emergency Contact", "1234567890"),
                true
            );

            var guests = new List<Guest>
            {
                new Guest(Id.Create(Guid.NewGuid()), GuestFullName.Create("John", "Doe"), DateOnly.FromDateTime(DateTime.Now.AddYears(-30)), "Male", GuestDocument.Create("Passport", "123456789"), GuestEmail.Create("john.doe@example.com"), "1234567890", reservation)
            };

            _reservationRepositoryMock.Setup(repo => repo.Get(reservationId)).ReturnsAsync(reservation);
            _guestRepositoryMock.Setup(repo => repo.GetGuestByReservation(reservationId)).ReturnsAsync(guests);

            // Act
            var result = await _reservationService.Get(reservationId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Reservation obtained successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Get_ShouldReturnErrorResponse_WhenReservationNotFound()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            _reservationRepositoryMock.Setup(repo => repo.Get(reservationId)).ReturnsAsync((Reservation)null);

            // Act
            var result = await _reservationService.Get(reservationId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Reservation not found", result.Message);
        }

        [Fact]
        public async Task SetStatus_ShouldReturnSuccessResponse_WhenStatusIsSet()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var status = true;

            _reservationRepositoryMock.Setup(repo => repo.SetStatus(reservationId, status)).Returns(Task.CompletedTask);

            // Act
            var result = await _reservationService.SetStatus(reservationId, status);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Status set successfully", result.Message);
        }

        [Fact]
        public async Task SetStatus_ShouldReturnErrorResponse_WhenErrorOccurs()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            var status = true;

            _reservationRepositoryMock.Setup(repo => repo.SetStatus(reservationId, status)).ThrowsAsync(new Exception("Error setting status"));

            // Act
            var result = await _reservationService.SetStatus(reservationId, status);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Error setting status: Error setting status", result.Message);
        }
    }
}
