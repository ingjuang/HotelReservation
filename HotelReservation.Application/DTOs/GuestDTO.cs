using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Application.DTOs
{
    public class GuestDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string DocumentType { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public Guid ReservationId { get; set; }
    }
}
