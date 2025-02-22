using HotelReservation.Application.Commands;
using HotelReservation.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest([FromBody] GuestDTO guestDto)
        {
            var command = new CreateGuestCommand(guestDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
