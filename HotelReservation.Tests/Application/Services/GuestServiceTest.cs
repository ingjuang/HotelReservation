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
    public class GuestServiceTests
    {
        private readonly Mock<IGuestRepository> _guestRepositoryMock;
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly GuestService _guestService;

        public GuestServiceTests()
        {
            _guestRepositoryMock = new Mock<IGuestRepository>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _guestService = new GuestService(_guestRepositoryMock.Object, _reservationRepositoryMock.Object, _roomRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateGuest_ShouldReturnSuccessResponse_WhenGuestIsCreated()
        {
            // Arrange
            var guestDTO = new GuestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "123456789",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                ReservationId = Guid.NewGuid()
            };

            var reservation = new Reservation(
                Id.Create(guestDTO.ReservationId),
                new Room(Id.Create(Guid.NewGuid()), new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true), RoomPrice.Create(100, 10), "Single", "First Floor", 2, true, true),
                DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                ContactInfo.Create("Emergency Contact", "1234567890"),
                true
            );

            var room = new Room(
                Id.Create(Guid.NewGuid()),
                new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2,
                true,
                true
            );

            var guest = new Guest(
                Id.Create(Guid.NewGuid()),
                GuestFullName.Create(guestDTO.FirstName, guestDTO.LastName),
                guestDTO.BirthDate,
                guestDTO.Gender,
                GuestDocument.Create(guestDTO.DocumentType, guestDTO.DocumentNumber),
                GuestEmail.Create(guestDTO.Email),
                guestDTO.Phone,
                reservation
            );

            _reservationRepositoryMock.Setup(repo => repo.Get(guestDTO.ReservationId)).ReturnsAsync(reservation);
            _roomRepositoryMock.Setup(repo => repo.Get(reservation.Room.Id)).ReturnsAsync(room);
            _guestRepositoryMock.Setup(repo => repo.GetGuestByReservation(reservation.Id)).ReturnsAsync(new List<Guest>());
            _guestRepositoryMock.Setup(repo => repo.CreateGuest(It.IsAny<Guest>())).ReturnsAsync(guest);

            // Act
            var result = await _guestService.CreateGuest(guestDTO);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Guest created successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task CreateGuest_ShouldReturnErrorResponse_WhenReservationNotFound()
        {
            // Arrange
            var guestDTO = new GuestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "123456789",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                ReservationId = Guid.NewGuid()
            };

            _reservationRepositoryMock.Setup(repo => repo.Get(guestDTO.ReservationId)).ReturnsAsync((Reservation)null);

            // Act
            var result = await _guestService.CreateGuest(guestDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Reservation not found", result.Message);
        }

        [Fact]
        public async Task CreateGuest_ShouldReturnErrorResponse_WhenRoomNotFound()
        {
            // Arrange
            var guestDTO = new GuestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "123456789",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                ReservationId = Guid.NewGuid()
            };

            var reservation = new Reservation(
                Id.Create(guestDTO.ReservationId),
                new Room(Id.Create(Guid.NewGuid()), new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true), RoomPrice.Create(100, 10), "Single", "First Floor", 2, true, true),
                DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                ContactInfo.Create("Emergency Contact", "1234567890"),
                true
            );

            _reservationRepositoryMock.Setup(repo => repo.Get(guestDTO.ReservationId)).ReturnsAsync(reservation);
            _roomRepositoryMock.Setup(repo => repo.Get(reservation.Room.Id)).ReturnsAsync((Room)null);

            // Act
            var result = await _guestService.CreateGuest(guestDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Room not found", result.Message);
        }

        [Fact]
        public async Task CreateGuest_ShouldReturnErrorResponse_WhenRoomIsFull()
        {
            // Arrange
            var guestDTO = new GuestDTO
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Gender = "Male",
                DocumentType = "Passport",
                DocumentNumber = "123456789",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                ReservationId = Guid.NewGuid()
            };

            var reservation = new Reservation(
                Id.Create(guestDTO.ReservationId),
                new Room(Id.Create(Guid.NewGuid()), new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true), RoomPrice.Create(100, 10), "Single", "First Floor", 2, true, true),
                DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                ContactInfo.Create("Emergency Contact", "1234567890"),
                true
            );

            var room = new Room(
                Id.Create(Guid.NewGuid()),
                new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2,
                true,
                true
            );

            var guests = new List<Guest>
            {
                new Guest(Id.Create(Guid.NewGuid()), GuestFullName.Create("Jane", "Doe"), DateOnly.FromDateTime(DateTime.Now.AddYears(-25)), "Female", GuestDocument.Create("Passport", "987654321"), GuestEmail.Create("jane.doe@example.com"), "0987654321", reservation),
                new Guest(Id.Create(Guid.NewGuid()), GuestFullName.Create("Jack", "Doe"), DateOnly.FromDateTime(DateTime.Now.AddYears(-20)), "Male", GuestDocument.Create("Passport", "123456789"), GuestEmail.Create("jack.doe@example.com"), "1234567890", reservation)
            };

            _reservationRepositoryMock.Setup(repo => repo.Get(guestDTO.ReservationId)).ReturnsAsync(reservation);
            _roomRepositoryMock.Setup(repo => repo.Get(reservation.Room.Id)).ReturnsAsync(room);
            _guestRepositoryMock.Setup(repo => repo.GetGuestByReservation(reservation.Id)).ReturnsAsync(guests);

            // Act
            var result = await _guestService.CreateGuest(guestDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Room is full", result.Message);
        }
    }
}
