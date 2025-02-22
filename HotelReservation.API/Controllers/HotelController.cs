using HotelReservation.Application.Commands;
using HotelReservation.Application.DTOs;
using HotelReservation.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTO hotelDto)
        {
            var command = new CreateHotelCommand(hotelDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}/enable")]
        public async Task<IActionResult> SetHotelEnable(Guid id, [FromBody] bool isEnabled)
        {
            var command = new SetHotelEnableCommand(id, isEnabled);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelDTO hotelDto)
        {
            var command = new UpdateHotelCommand(hotelDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetHotelByCity(string city)
        {
            var query = new GetHotelByCityQuery(city);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
