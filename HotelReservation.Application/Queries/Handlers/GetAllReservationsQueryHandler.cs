using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Queries.Handlers
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, PetitionResponse>
    {
        private readonly IReservationService _reservationService;

        public GetAllReservationsQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<PetitionResponse> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            return await _reservationService.Get();
        }
    }
}
