using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;
using MediatR;

namespace HotelReservation.Application.Commands
{
    public class CreateHotelCommand : IRequest<PetitionResponse>
    {
        public HotelDTO HotelDTO { get; set; }

        public CreateHotelCommand(HotelDTO hotelDTO)
        {
            HotelDTO = hotelDTO;
        }
    }
}
