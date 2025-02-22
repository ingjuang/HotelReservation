using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infraestructure.DTOs
{
    public class HotelDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("is_enabled")]
        public bool IsEnabled { get; set; }
    }
}
