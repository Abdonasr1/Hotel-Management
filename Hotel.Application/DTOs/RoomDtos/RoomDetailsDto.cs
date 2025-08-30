using Hotel.Domain.Enims;

namespace Hotel.Application.DTOs.RoomDtos
{
    public class RoomDetailsDto
    {
        public  int Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public RoomType? Type { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        //public RoomFacilities Facilities { get; set; }
        public string? Features { get; set; }

        public string? ImageUrl { get; set; }

    }
}
