using Hotel.Application.DTOs.RoomDtos;

namespace Hotel.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDetailsDto>> GetAllRoomsAsync();
        Task DeleteRoomAsync(int id);
        Task UpdateRoomAsync(int id, UpdateRoomDto room);
        Task AddRoomAsync(CreateRoomDto room);
        Task<RoomDetailsDto> GetRoomByIdAsync(int id);
        Task<IEnumerable<RoomDetailsDto>> GetAvailableRoomsAsync();

    }
}
