using Hotel.Application.DTOs.GuestDtos;
using Hotel.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController(IGuestService guestService) : ControllerBase
    {
        private readonly IGuestService _guestService = guestService;

        [HttpGet]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<ActionResult<IEnumerable<GuestDetailsDto>>> GetAllGuest() => Ok(await _guestService.GetAllGuestAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<ActionResult<GuestDetailsDto>> GetByIdGuest(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);

            if (guest == null)
            {
                return NotFound("Invalid Guest ID");
            }

            return Ok(guest);
        }

        [HttpPut("UpDate")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<ActionResult> UpDateGuest(int id, [FromBody] UpDateGuestDto guest)
        {
            var upGuest = _guestService.GetGuestByIdAsync(id);
            if (upGuest == null)
                return NotFound("Guest not found");

            await _guestService.UpdateGuestAsync(id, guest);
            return Ok();

        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<ActionResult> AddGuest([FromBody] CreateGuestDto guest)
        {
            if (guest == null)
            {
                return BadRequest("Guest cannot be null");
            }
            await _guestService.AddGuestAsync(guest);
            return Ok("Guest added successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteGuest(int id)
        {
            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound("Guest not found");
            }
            await _guestService.DeleteGuestAsync(id);
            return Ok("Guest deleted successfully");
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> SearchGuest([FromQuery] string? phone, [FromQuery] string? email)
        {
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
                return BadRequest("Please provide either phone or email.");

            var guest = await _guestService.SearchGuestAsync(phone, email);
            if (guest == null)
                return NotFound("Guest not found.");

            return Ok(guest);
        }
    }
}
