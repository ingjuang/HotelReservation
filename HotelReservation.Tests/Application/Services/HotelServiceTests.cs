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
    public class HotelServiceTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly HotelService _hotelService;

        public HotelServiceTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _hotelService = new HotelService(_hotelRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateHotel_ShouldReturnSuccessResponse_WhenHotelIsCreated()
        {
            // Arrange
            var hotelDTO = new HotelDTO
            {
                Name = "Test Hotel",
                Address = "123 Test St",
                City = "Test City",
                Country = "Test Country",
                IsEnabled = true
            };

            var hotel = new Hotel(
                Id.Create(Guid.NewGuid()),
                HotelName.Create(hotelDTO.Name),
                HotelLocation.Create(hotelDTO.Country, hotelDTO.City, hotelDTO.Address),
                hotelDTO.IsEnabled
            );

            _hotelRepositoryMock.Setup(repo => repo.CreateHotel(It.IsAny<Hotel>())).ReturnsAsync(hotel);

            // Act
            var result = await _hotelService.CreateHotel(hotelDTO);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Hotel created successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task SetEnable_ShouldReturnSuccessResponse_WhenHotelIsUpdated()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = new Hotel(
                Id.Create(hotelId),
                HotelName.Create("Test Hotel"),
                HotelLocation.Create("Test Country", "Test City", "123 Test St"),
                true
            );

            _hotelRepositoryMock.Setup(repo => repo.Get(hotelId)).ReturnsAsync(hotel);
            _hotelRepositoryMock.Setup(repo => repo.SetEnable(hotelId, false)).Returns(Task.CompletedTask);

            // Act
            var result = await _hotelService.SetEnable(hotelId, false);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Hotel updated successfully", result.Message);
        }

        [Fact]
        public async Task UpdateHotel_ShouldReturnSuccessResponse_WhenHotelIsUpdated()
        {
            // Arrange
            var hotelDTO = new HotelDTO
            {
                Id = Guid.NewGuid(),
                Name = "Updated Hotel",
                Address = "123 Updated St",
                City = "Updated City",
                Country = "Updated Country",
                IsEnabled = true
            };

            var hotel = new Hotel(
                Id.Create(hotelDTO.Id),
                HotelName.Create(hotelDTO.Name),
                HotelLocation.Create(hotelDTO.Country, hotelDTO.City, hotelDTO.Address),
                hotelDTO.IsEnabled
            );

            _hotelRepositoryMock.Setup(repo => repo.Get(hotelDTO.Id)).ReturnsAsync(hotel);
            _hotelRepositoryMock.Setup(repo => repo.UpdateHotel(It.IsAny<Hotel>())).ReturnsAsync(hotel);

            // Act
            var result = await _hotelService.UpdateHotel(hotelDTO);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Hotel updated successfully", result.Message);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task GetHotelByCity_ShouldReturnSuccessResponse_WhenHotelsAreFound()
        {
            // Arrange
            var city = "Test City";
            var hotels = new List<Hotel>
            {
                new Hotel(
                    Id.Create(Guid.NewGuid()),
                    HotelName.Create("Test Hotel 1"),
                    HotelLocation.Create("Test Country", city, "123 Test St"),
                    true
                ),
                new Hotel(
                    Id.Create(Guid.NewGuid()),
                    HotelName.Create("Test Hotel 2"),
                    HotelLocation.Create("Test Country", city, "456 Test St"),
                    true
                )
            };

            _hotelRepositoryMock.Setup(repo => repo.GetByCity(city)).ReturnsAsync(hotels);

            // Act
            var result = await _hotelService.GetHotelByCity(city);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Hotels found", result.Message);
            Assert.NotNull(result.Result);
        }
    }
}
