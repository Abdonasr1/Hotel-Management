using Hotel.Application.DTOs.GuestDtos;

namespace Hotel.Application.Interfaces
{
    public interface IGuestService
    {
        Task<IEnumerable<GuestDetailsDto>> GetAllGuestAsync();
        Task<GuestDetailsDto> GetGuestByIdAsync(int id);
        Task AddGuestAsync(CreateGuestDto guest);

        Task DeleteGuestAsync(int id);
        Task UpdateGuestAsync(int id, UpDateGuestDto guest);
        Task<GuestDetailsDto?> SearchGuestAsync(string? phone, string? email);
    }
}
