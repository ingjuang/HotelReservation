using HotelReservation.Util.Reponses;
using MediatR;

namespace HotelReservation.Application.Commands
{
    public class SetHotelEnableCommand : IRequest<PetitionResponse>
    {
        public Guid HotelId { get; set; }
        public bool IsEnabled { get; set; }

        public SetHotelEnableCommand(Guid hotelId, bool isEnabled)
        {
            HotelId = hotelId;
            IsEnabled = isEnabled;
        }
    }
}
