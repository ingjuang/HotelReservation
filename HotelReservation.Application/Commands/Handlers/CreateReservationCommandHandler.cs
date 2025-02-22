using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands.Handlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, PetitionResponse>
    {
        private readonly IReservationService _reservationService;

        public CreateReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<PetitionResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            return await _reservationService.CreateReservation(request.Reservation);
        }
    }
}
