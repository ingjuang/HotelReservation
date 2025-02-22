using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Application.DTOs
{
    public class ReservationDTO
    {
        public Guid Id { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public List<string> GuestIds { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public string EmergencyContactFullName { get; set; }

        [Required]
        [Phone]
        public string EmergencyContactPhone { get; set; }
        [Required]
        public List<GuestDTO> Guests { get; set; }
    }
}
