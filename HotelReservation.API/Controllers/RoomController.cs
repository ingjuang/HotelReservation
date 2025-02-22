using HotelReservation.Application.Commands.Handlers;
using HotelReservation.Application.Commands;
using HotelReservation.Application.Queries;
using HotelReservation.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelReservation.Application.DTOs;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDTO roomDto)
        {
            var command = new CreateRoomCommand(roomDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}/enable")]
        public async Task<IActionResult> SetRoomEnable(Guid id, [FromBody] bool isEnabled)
        {
            var command = new SetRoomEnableCommand(id, isEnabled);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{roomId}/hotel/{hotelId}")]
        public async Task<IActionResult> SetRoomToHotel(Guid roomId, Guid hotelId)
        {
            var command = new SetRoomToHotelCommand(roomId, hotelId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotel(Guid hotelId)
        {
            var query = new GetRoomsByHotelQuery(hotelId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAvailableRooms([FromQuery] DateRange stayPeriod, [FromQuery] int guests, [FromQuery] string city)
        {
            var query = new SearchAvailableRoomsQuery(stayPeriod, guests, city);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
