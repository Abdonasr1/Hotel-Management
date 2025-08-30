using AutoMapper;
using Hotel.Application.DTOs.ReservationDtos;
using Hotel.Application.Interfaces;
using Hotel.Domain.Enims;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;
using Microsoft.Extensions.Logging;
namespace Hotel.Application.Services
{
    public class ReservationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReservationService> logger) : IReservationService
    {
        private readonly ILogger<ReservationService> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


        public async Task<IEnumerable<ReservationDetailsDto>> GetAllReservationAsync() => _mapper.Map<List<ReservationDetailsDto>>(await _unitOfWork.ReservationRepository.GetAllAsync(
            null,
            r => r.Room,
            r => r.Guest
            ));


        public async Task<ReservationDetailsDto> GetReservationByIdAsync(int id)
        {
            if (id <=0 )
            {
                return null;
            }
            return _mapper.Map<ReservationDetailsDto>(await _unitOfWork.ReservationRepository.GetByIdAsync(
            r => r.Id == id,
            r => r.Room,
            r => r.Guest));
        }


        public async Task AddReservationAsync(CreateReservationDto reservationDetailsDto)
        {
            var newReservation = _mapper.Map<Reservation>(reservationDetailsDto);


            newReservation.TotalPrice = await CalculateReservationTotalPrice(newReservation);


            await _unitOfWork.ReservationRepository.CreateAsync(newReservation);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("A new reservation number {ReservationId} was created for guest {GuestId} in room {RoomId} from {CheckIn} to {CheckOut}.", newReservation.Id, newReservation.GuestId, newReservation.RoomId, newReservation.CheckInDate, newReservation.CheckOutDate);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(r => r.Id == id);
            if (reservation != null)
            {
                _unitOfWork.ReservationRepository.RemoveAsync(reservation);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto)
        {
            var existingReservation = await _unitOfWork.ReservationRepository.GetByIdAsync(r => r.Id == id);

            _mapper.Map(updateReservationDto, existingReservation);
            _unitOfWork.ReservationRepository.UpdateAsync(existingReservation);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<decimal> CalculateReservationTotalPrice(Reservation reservation)
        {
            int days = (reservation.CheckOutDate - reservation.CheckInDate).Days;
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(r => r.Id ==  reservation.RoomId);
            

            return days * room.PricePerNight;
        }

        public async Task<ReservationDetailsDto?> FindReservationAsync(int? roomId, int? guestId)
        {
            var reservation = await _unitOfWork.ReservationRepository
                .SearchReservationsAsync(roomId, guestId,
                    r => r.Room,
                    r => r.Guest);

            return _mapper.Map<ReservationDetailsDto>(reservation);
        }

        public async Task<bool> UpdateReservationStatusAsync(int reservationId, ReservationStatus newStatus)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(r => r.Id == reservationId);
            if (reservation == null)
            {
                _logger.LogWarning("Attempt to update status of a non-existent reservation number {ReservationId}", reservationId);
                return false;
            }

            reservation.Status = newStatus;
            _unitOfWork.ReservationRepository.UpdateAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Reservation number {ReservationId} status updated to {NewStatus}", reservationId, newStatus);
            return true;
        }



    }
}
