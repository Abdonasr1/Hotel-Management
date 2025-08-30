using Hotel.Domain.Enims;
namespace Hotel.Application.DTOs.User
{
    public class RegisterUserDto
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; } = UserRole.Guest; // Default role for new users
    }
}
