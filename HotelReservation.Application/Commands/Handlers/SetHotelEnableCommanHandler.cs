using HotelReservation.Application.Services.Interfaces;
using HotelReservation.Domain.Repositories;
using HotelReservation.Util.Reponses;
using MediatR;

namespace HotelReservation.Application.Commands.Handlers
{
    public class SetHotelEnableCommandHandler : IRequestHandler<SetHotelEnableCommand, PetitionResponse>
    {
        private readonly IHotelService _hotelService;

        public SetHotelEnableCommandHandler(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<PetitionResponse> Handle(SetHotelEnableCommand request, CancellationToken cancellationToken)
        {
            PetitionResponse response  = await _hotelService.SetEnable(request.HotelId, request.IsEnabled);
            return response;
        }
    }
}
