using Hotel.Domain.Enims;

namespace Hotel.Application.DTOs.ReservationDtos
{
    public class UpdateReservationDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
