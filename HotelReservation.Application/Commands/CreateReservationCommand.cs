using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Commands
{
    public class CreateReservationCommand : IRequest<PetitionResponse>
    {
        public ReservationDTO Reservation { get; set; }

        public CreateReservationCommand(ReservationDTO reservation)
        {
            Reservation = reservation;
        }
    }
}
