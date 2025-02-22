using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infraestructure.DTOs
{
    public class ReservationDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("room_id")]
        public Guid RoomId { get; set; }

        [BsonElement("stay_period_start")]
        public DateTime StayPeriodStart { get; set; }

        [BsonElement("stay_period_end")]
        public DateTime StayPeriodEnd { get; set; }

        [BsonElement("status")]
        public bool Status { get; set; }

        [BsonElement("emergency_contact_full_name")]
        public string EmergencyContactFullName { get; set; }

        [BsonElement("emergency_contact_phone")]
        public string EmergencyContactPhone { get; set; }
    }
}
