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
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly RoomService _roomService;

        public RoomServiceTests()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _roomService = new RoomService(_roomRepositoryMock.Object, _hotelRepositoryMock.Object, _reservationRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRoom_ShouldReturnSuccessResponse_WhenRoomIsCreated()
        {
            // Arrange
            var roomDTO = new RoomDTO
            {
                HotelId = Guid.NewGuid(),
                Price = 100,
                Taxes = 10,
                Type = "Single",
                IsAvailable = true,
                IsEnabled = true,
                Location = "First Floor",
                MaxGuests = 2
            };

            var hotel = new Hotel(
                Id.Create(roomDTO.HotelId),
                HotelName.Create("Test Hotel"),
                HotelLocation.Create("Test Country", "Test City", "123 Test St"),
                true
            );

            var room = new Room(
                Id.Create(Guid.NewGuid()),
                hotel,
                RoomPrice.Create(roomDTO.Price, roomDTO.Taxes),
                roomDTO.Type,
                roomDTO.Location,
                roomDTO.MaxGuests,
                roomDTO.IsAvailable,
                roomDTO.IsEnabled
            );

            _hotelRepositoryMock.Setup(repo => repo.Get(roomDTO.HotelId)).ReturnsAsync(hotel);
            _roomRepositoryMock.Setup(repo => repo.CreateRoom(It.IsAny<Room>())).ReturnsAsync(room);

            // Act
            var result = await _roomService.CreateRoom(roomDTO);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Room created successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task GetByHotelRooms_ShouldReturnSuccessResponse_WhenRoomsAreFound()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var rooms = new List<Room>
            {
                new Room(
                    Id.Create(Guid.NewGuid()),
                    new Hotel(Id.Create(hotelId), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", "Test City", "123 Test St"), true),
                    RoomPrice.Create(100, 10),
                    "Single",
                    "First Floor",
                    2,
                    true,
                    true
                )
            };

            _roomRepositoryMock.Setup(repo => repo.GetByHotelRooms(hotelId)).ReturnsAsync(rooms);

            // Act
            var result = await _roomService.GetByHotelRooms(hotelId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Rooms found", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task SearchAvalibleRooms_ShouldReturnSuccessResponse_WhenRoomsAreFound()
        {
            // Arrange
            var stayPeriod = DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)));
            var city = "Test City";
            var rooms = new List<Room>
            {
                new Room(
                    Id.Create(Guid.NewGuid()),
                    new Hotel(Id.Create(Guid.NewGuid()), HotelName.Create("Test Hotel"), HotelLocation.Create("Test Country", city, "123 Test St"), true),
                    RoomPrice.Create(100, 10),
                    "Single",
                    "First Floor",
                    2,
                    true,
                    true
                )
            };

            _roomRepositoryMock.Setup(repo => repo.SearchAvalibleRooms(stayPeriod, 2, city)).ReturnsAsync(rooms);
            _reservationRepositoryMock.Setup(repo => repo.HasActiveReservation(It.IsAny<Guid>(), stayPeriod)).ReturnsAsync(false);

            // Act
            var result = await _roomService.SearchAvalibleRooms(stayPeriod, 2, city);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Rooms found", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task SetEnable_ShouldReturnSuccessResponse_WhenRoomIsUpdated()
        {
            // Arrange
            var roomId = Guid.NewGuid();

            _roomRepositoryMock.Setup(repo => repo.SetEnable(roomId, true)).Returns(Task.CompletedTask);

            // Act
            var result = await _roomService.SetEnable(roomId, true);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Room updated successfully", result.Message);
        }

        [Fact]
        public async Task SetRoomToHotel_ShouldReturnSuccessResponse_WhenRoomIsSetToHotel()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var hotelId = Guid.NewGuid();

            var hotel = new Hotel(
                Id.Create(hotelId),
                HotelName.Create("Test Hotel"),
                HotelLocation.Create("Test Country", "Test City", "123 Test St"),
                true
            );

            var room = new Room(
                Id.Create(roomId),
                hotel,
                RoomPrice.Create(100, 10),
                "Single",
                "First Floor",
                2,
                true,
                true
            );

            _roomRepositoryMock.Setup(repo => repo.Get(roomId)).ReturnsAsync(room);
            _hotelRepositoryMock.Setup(repo => repo.Get(hotelId)).ReturnsAsync(hotel);
            _roomRepositoryMock.Setup(repo => repo.SetRoomToHotel(roomId, hotelId)).Returns(Task.CompletedTask);

            // Act
            var result = await _roomService.SetRoomToHotel(roomId, hotelId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Room set to hotel successfully", result.Message);
        }
    }
}
