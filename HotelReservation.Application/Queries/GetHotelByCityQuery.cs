using HotelReservation.Util.Reponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Queries
{
    public class GetHotelByCityQuery : IRequest<PetitionResponse>
    {
        public string City { get; set; }

        public GetHotelByCityQuery(string city)
        {
            City = city;
        }
    }
}
