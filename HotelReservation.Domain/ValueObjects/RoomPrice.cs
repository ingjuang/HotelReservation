using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record RoomPrice
    {
        public decimal Price { get; private set; }
        public decimal Taxes { get; private set; }

        internal RoomPrice(decimal price, decimal taxes)
        {
            Price = price;
            Taxes = taxes;
        }
        public static RoomPrice Create(decimal price, decimal taxes)
        {
            Validate(price, taxes);
            return new RoomPrice(price, taxes);
        }

        private static void Validate(decimal price, decimal taxes)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            if (taxes < 0)
            {
                throw new ArgumentException("Taxes cannot be negative");
            }
        }
    }
}
