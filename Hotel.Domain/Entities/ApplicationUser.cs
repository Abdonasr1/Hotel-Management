using Hotel.Domain.Enims;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public UserRole Role { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
