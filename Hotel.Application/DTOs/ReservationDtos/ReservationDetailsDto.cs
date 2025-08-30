using Hotel.Domain.Enims;

namespace Hotel.Application.DTOs.ReservationDtos
{
    public class ReservationDetailsDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public decimal PricePerNighr { get; set; }

        public int GuestId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? NationalId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
