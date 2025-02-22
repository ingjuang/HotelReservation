using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Application.DTOs
{
    public class RoomDTO
    {
        public Guid Id { get; set; }

        [Required]
        public Guid HotelId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Taxes { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int MaxGuests { get; set; }
    }
}
