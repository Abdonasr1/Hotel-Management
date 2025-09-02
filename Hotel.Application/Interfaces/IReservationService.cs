using Hotel.Application.DTOs.ReservationDtos;
using Hotel.Domain.Enims;

namespace Hotel.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDetailsDto>> GetAllReservationAsync();
        Task<ReservationDetailsDto> GetReservationByIdAsync(int id);
        Task DeleteReservationAsync(int id);
        Task UpdateReservationAsync(int id, UpdateReservationDto reservationDetailsDto);
        Task AddReservationAsync(CreateReservationDto reservationDetailsDto);
        Task<ReservationDetailsDto?> FindReservationAsync(int? guestId);
        Task<bool> UpdateReservationStatusAsync(int reservationId, ReservationStatus newStatus);
    }
}
