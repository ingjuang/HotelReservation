using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Application.DTOs
{
    public class HotelDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

    }
}
