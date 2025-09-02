using AutoMapper;
using Hotel.Application.DTOs.GuestDtos;
using Hotel.Application.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;

namespace Hotel.Application.Services
{
    public class GuestService(IUnitOfWork unitOfWork,IMapper mapper ) : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<GuestDetailsDto>> GetAllGuestAsync() => _mapper.Map<List<GuestDetailsDto>>(await _unitOfWork.GuestRepository.GetAllAsync(
            null,
            g => g.Reservations
            ));


        public async Task<GuestDetailsDto> GetGuestByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return _mapper.Map<GuestDetailsDto>(await _unitOfWork.GuestRepository
                .GetByIdAsync(g => g.Id == id,
                              g => g.Reservations));
        }

        public async Task AddGuestAsync(CreateGuestDto guest)
        {
            var guestEntity = _mapper.Map<Guest>(guest);
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest), "Guest cannot be null");
            }
            await _unitOfWork.GuestRepository.CreateAsync(guestEntity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteGuestAsync(int id)
        {
            var guest = await _unitOfWork.GuestRepository.GetByIdAsync(g => g.Id == id);
            if (guest == null)
                throw new KeyNotFoundException($"Guest with Id {id} not found.");
            _unitOfWork.GuestRepository.RemoveAsync(guest);
            await _unitOfWork.SaveChangesAsync();
        }

        public  async Task UpdateGuestAsync(int id, UpDateGuestDto guest)
        {
            var existingGuest = await _unitOfWork.GuestRepository.GetByIdAsync(g => g.Id == id);
            _mapper.Map(guest, existingGuest);
            _unitOfWork.GuestRepository.UpdateAsync(existingGuest);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<GuestDetailsDto?> SearchGuestAsync(string? phone, string? email)
        {
            var guest = _mapper.Map<GuestDetailsDto>(await _unitOfWork.GuestRepository.GetByPhoneOrEmailAsync(
                phone, 
                email,
                g => g.Reservations
                ));
            return guest != null ? guest : null;
        }
    }
}
