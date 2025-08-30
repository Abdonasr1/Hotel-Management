
namespace Hotel.Application.DTOs.GuestDtos
{
    public class UpDateGuestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? NationalId { get; set; }
    }
}
