using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infraestructure.DTOs
{
    public class GuestDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("birth_date")]
        public DateOnly BirthDate { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("document_type")]
        public string DocumentType { get; set; }

        [BsonElement("document_number")]
        public string DocumentNumber { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("reservation_id")]
        public Guid ReservationId { get; set; }
    }

}
