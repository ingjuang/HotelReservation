using HotelReservation.Application.DTOs;
using HotelReservation.Util.Reponses;
using MediatR;
namespace HotelReservation.Application.Commands
{
    public class UpdateHotelCommand : IRequest<PetitionResponse>
    {
        public HotelDTO HotelDTO { get; set; }

        public UpdateHotelCommand(HotelDTO hotelDTO)
        {
            HotelDTO = hotelDTO;
        }
    }
}
