using Microsoft.AspNetCore.Mvc;
using CinemaBackend.Data;
using CinemaBackend.DTO;
using CinemaBackend.Models;
using Microsoft.EntityFrameworkCore;


namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservationsApi
        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();
            var reservationDTOs = reservations.Select(r => new ReservationDTO
            {
                Id = r.Id,
                // Map other properties as needed
            }).ToList();
            return Ok(reservationDTOs);
        }

        // GET: api/ReservationsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            var reservationDTO = new ReservationDTO
            {
                Id = reservation.Id,
                // Map other properties as needed
            };
            return Ok(reservationDTO);
        }

        // POST: api/ReservationsApi
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDTO reservationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reservation = new Reservation
            {
                // Map properties from reservationDTO to reservation
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            reservationDTO.Id = reservation.Id;
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservationDTO);
        }

        // PUT: api/ReservationsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationDTO reservationDTO)
        {
            if (id != reservationDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            // Update reservation properties from reservationDTO
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ReservationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}