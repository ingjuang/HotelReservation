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
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, PetitionResponse>
    {
        private readonly IReservationService _reservationService;

        public GetReservationByIdQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<PetitionResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _reservationService.Get(request.Id);
        }
    }
}
