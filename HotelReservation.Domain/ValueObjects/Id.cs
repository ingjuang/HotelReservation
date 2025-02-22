using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record Id
    {
        public Guid Value { get; }

        internal Id(Guid value)
        {
            Value = value;
        }

        public static Id Create(Guid value)
        {
            return new Id(value);
        }

        public static implicit operator Guid(Id id)
        {
            return id.Value;
        }
    }
}
