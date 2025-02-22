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
    public class SetReservationStatusCommandHandler : IRequestHandler<SetReservationStatusCommand, PetitionResponse>
    {
        private readonly IReservationService _reservationService;

        public SetReservationStatusCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<PetitionResponse> Handle(SetReservationStatusCommand request, CancellationToken cancellationToken)
        {
            return await _reservationService.SetStatus(request.ReservationId, request.Status);
        }
    }
}
