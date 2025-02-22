using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands
{
    public class SetReservationStatusCommand : IRequest<PetitionResponse>
    {
        public Guid ReservationId { get; set; }
        public bool Status { get; set; }

        public SetReservationStatusCommand(Guid reservationId, bool status)
        {
            ReservationId = reservationId;
            Status = status;
        }
    }
}
