using Microsoft.AspNetCore.Mvc;
using Hotel.Application.Interfaces;
using AutoMapper;
using Hotel.Application.DTOs.RoomDtos;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Ensure that the controller is protected by authentication
    public class RoomsController(IRoomService roomService, IMapper mapper) : ControllerBase
    {
        private readonly IRoomService _roomService = roomService;

        // GET: api/Rooms
        [HttpGet("Rooms")]
        [Authorize(Roles = "Admin, Receptionist, Guest")] 
        public async Task<ActionResult<IEnumerable<RoomDetailsDto>>> GetRooms() => Ok(await _roomService.GetAllRoomsAsync());


        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Receptionist, Guest")]
        public async Task<ActionResult<RoomDetailsDto>> GetByIdRoom(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound("Invalid Room ID");
            }

            return Ok(room);
        }



        //PUT: api/Rooms/5
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomDto updateDto)
        {

            var existingRoom = await _roomService.GetRoomByIdAsync(id);

            if (existingRoom == null)
                return NotFound("Room not found");

            await _roomService.UpdateRoomAsync(id, updateDto);

            return Ok("Room updated successfully");
        }



        //POST: api/Rooms
        [HttpPost("AddRoom")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddRoom([FromBody] CreateRoomDto room)
        {
            if (room == null)
            {
                return BadRequest("Room data is null.");
            }
            await _roomService.AddRoomAsync(room);
            return Ok(room);
        }



        // DELETE: api/Rooms/5
        [HttpDelete("DeleteRoom")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomService.DeleteRoomAsync(id);

            return Ok();
        }

        // GET: api/Rooms/Available
        [HttpGet("Available")]
        [Authorize(Roles = "Admin, Receptionist, Guest")]
        public async Task<ActionResult<IEnumerable<RoomDetailsDto>>> GetAvailableRooms()
        {
            var availableRooms = await _roomService.GetAllRoomsAsync();
            if (availableRooms == null || !availableRooms.Any())
            {
                return NotFound("No available rooms found.");
            }
            return Ok(availableRooms.Where(r => r.IsAvailable));

        }
    }
}
