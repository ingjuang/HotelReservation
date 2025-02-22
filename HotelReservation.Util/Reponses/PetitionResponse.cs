using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Util.Reponses
{
    public class PetitionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
