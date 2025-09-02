using AutoMapper;
using Hotel.Application.DTOs.RoomDtos;
using Hotel.Application.Interfaces;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;

namespace Hotel.Application.Services
{
    public class RoomService(IUnitOfWork unitOfWork, IMapper mapper ) : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper ;


        // Example method to get all rooms
        public async Task<IEnumerable<RoomDetailsDto>> GetAllRoomsAsync() => _mapper.Map<List<RoomDetailsDto>>(await _unitOfWork.RoomRepository.GetAllAsync());

        // Example method to get a room by ID
        public async Task<RoomDetailsDto> GetRoomByIdAsync(int id)
        {
            if (id <= 0 )
                return null;

            return _mapper.Map<RoomDetailsDto>(await _unitOfWork.RoomRepository.GetByIdAsync(r => r.Id == id));
        }
        

        // Example method to add a new room
        public async Task AddRoomAsync(CreateRoomDto room)
        {
            var newroom = _mapper.Map<Room>(room);
            newroom.IsAvailable = true; // Assuming new rooms are available by default
            if (newroom == null)
                throw new ArgumentNullException(nameof(newroom), "Room cannot be null");
            await _unitOfWork.RoomRepository.CreateAsync(newroom);
            await _unitOfWork.SaveChangesAsync();
        }





        // Example method to update a room  
        public async Task UpdateRoomAsync(int id, UpdateRoomDto updateDto)
        {
            var existingRoom = await _unitOfWork.RoomRepository.GetByIdAsync(r => r.Id == id);
            // انسخ البيانات الجديدة داخل الكيان الموجود، بدون تغيير الـ Id
            _mapper.Map(updateDto, existingRoom);

            _unitOfWork.RoomRepository.UpdateAsync(existingRoom);
            await _unitOfWork.SaveChangesAsync();
        }





        // Example method to delete a room
        public async Task DeleteRoomAsync(int id)
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(r => r.Id == id);
            if (room == null)
                throw new KeyNotFoundException($"Room with Id {id} not found.");
            
            _unitOfWork.RoomRepository.RemoveAsync(room);
            await _unitOfWork.SaveChangesAsync();
            
        }


        //Show only available rooms (IsAvailable = true)
        public async Task<IEnumerable<RoomDetailsDto>> GetAvailableRoomsAsync()
        {
            var availableRooms = await _unitOfWork.RoomRepository
                .GetAllAsync(r => r.IsAvailable == true);

            return _mapper.Map<List<RoomDetailsDto>>(availableRooms);
        }
    }
}
