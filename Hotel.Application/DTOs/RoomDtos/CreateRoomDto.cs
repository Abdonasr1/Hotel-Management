using Hotel.Domain.Enims;

namespace Hotel.Application.DTOs.RoomDtos
{
    public class CreateRoomDto
    {
        public string? Number { get; set; }
        public RoomType? Type { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }

        public string? ImageUrl { get; set; }

    }
}
