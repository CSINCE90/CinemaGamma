using Microsoft.AspNetCore.Mvc;

using CinemaBackend.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using CinemaBackend.Services;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaApiController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemaApiController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaDTO>>> GetCinemas()
        {
            var cinemaDTOs = await _cinemaService.GetAllCinemasAsync();
            return Ok(cinemaDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CinemaDTO>> GetCinema(int id)
        {
            var cinemaDTO = await _cinemaService.GetCinemaByIdAsync(id);
            if (cinemaDTO == null)
            {
                return NotFound();
            }
            return Ok(cinemaDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CinemaDTO>> CreateCinema([FromBody] CinemaDTO cinemaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCinemaDTO = await _cinemaService.CreateCinemaAsync(cinemaDTO);
            return CreatedAtAction(nameof(GetCinema), new { id = createdCinemaDTO.Id }, createdCinemaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCinema(int id, [FromBody] CinemaDTO cinemaDTO)
        {
            if (id != cinemaDTO.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the body.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cinemaService.UpdateCinemaAsync(cinemaDTO);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var result = await _cinemaService.DeleteCinemaAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
