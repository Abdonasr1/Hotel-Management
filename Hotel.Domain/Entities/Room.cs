using Hotel.Domain.Enims;

namespace Hotel.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public RoomType? Type { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }

        public string? Features { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
