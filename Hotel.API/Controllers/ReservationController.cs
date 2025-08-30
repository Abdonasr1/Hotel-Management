using Hotel.Application.DTOs.ReservationDtos;
using Hotel.Application.Interfaces;
using Hotel.Domain.Enims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(IReservationService reservationService) : ControllerBase
    {

        private readonly IReservationService _reservationService = reservationService;


        [HttpGet("Reservation")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<ActionResult<IEnumerable<ReservationDetailsDto>>> GetAll() => Ok(await _reservationService.GetAllReservationAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<ActionResult<ReservationDetailsDto>> GetByIdReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin, Receptionist, Guest")]
        public async Task<ActionResult> AddReservation([FromBody] CreateReservationDto createReservationDto)
        {
            
            if (createReservationDto == null)
                return BadRequest("");
            await _reservationService.AddReservationAsync(createReservationDto);
            
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return Ok();
        }

        //PUT: api/Rooms/5
        [HttpPut("Update")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto updateReservation)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound("Reservation not found");

            await _reservationService.UpdateReservationAsync(id, updateReservation);
            return Ok("Reservation Updated Successfully");
        }


        [HttpGet("search")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> SearchReservation([FromQuery] int? roomId, [FromQuery] int? guestId)
        {
            var reservation = await _reservationService.FindReservationAsync(roomId, guestId);

            if (reservation == null)
                return NotFound("No reservation found matching the criteria.");

            return Ok(reservation);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin, Receptionist")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ReservationStatus newStatus)
        {
            var success = await _reservationService.UpdateReservationStatusAsync(id, newStatus);
            if (!success)
                return NotFound($"Reservation with id {id} not found.");

            return NoContent(); 
        }


    }
}
