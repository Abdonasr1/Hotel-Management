using Hotel.Domain.Enims;

namespace Hotel.Application.DTOs.ReservationDtos
{
    public class CreateReservationDto
    {
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
