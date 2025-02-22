using HotelReservation.Application.Commands;
using HotelReservation.Application.DTOs;
using HotelReservation.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDTO reservationDto)
        {
            var command = new CreateReservationCommand(reservationDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SetReservationStatus(Guid id, [FromBody] bool status)
        {
            var command = new SetReservationStatusCommand(id, status);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var query = new GetAllReservationsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
