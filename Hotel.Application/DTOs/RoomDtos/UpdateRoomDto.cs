using Hotel.Domain.Enims;

namespace Hotel.Application.DTOs.RoomDtos
{
    public class UpdateRoomDto
    {
        public string? Number { get; set; }
        public RoomType? Type { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string? Features { get; set; }

        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

    }
}
