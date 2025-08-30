

namespace Hotel.Application.DTOs.GuestDtos
{
    public class GuestDetailsDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? NationalId { get; set; }

        public int? ReservationId { get; set; }
        public int? RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

}
