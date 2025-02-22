using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record DateRange
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        internal DateRange(DateOnly startDate, DateOnly endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public static DateRange Create(DateOnly startDate, DateOnly endDate)
        {
            Validate(startDate, endDate);
            return new DateRange(startDate, endDate);
        }

        private static void Validate(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be greater than end date");
            }
        }
    }
}
