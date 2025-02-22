using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infraestructure.DTOs
{
    public class RoomDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("hotel_id")]
        public Guid HotelId { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("taxes")]
        public decimal Taxes { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("is_available")]
        public bool IsAvailable { get; set; }

        [BsonElement("is_enabled")]
        public bool IsEnabled { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("max_guests")]
        public int MaxGuests { get; set; }
    }
}
