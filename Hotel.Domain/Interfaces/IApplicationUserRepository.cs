using Hotel.Domain.Entities;

namespace Hotel.Domain.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetByEmail(string email);
        Task<ApplicationUser> GetByIdAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
