using Microsoft.AspNetCore.Mvc;
using CinemaBackend.Services;
using CinemaBackend.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriumApiController : ControllerBase
    {
        private readonly IAuditoriumService _auditoriumService;

        public AuditoriumApiController(IAuditoriumService auditoriumService)
        {
            _auditoriumService = auditoriumService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditoriumDTO>>> GetAuditoriums()
        {
            var auditoriumDTOs = await _auditoriumService.GetAllAuditoriumsAsync();
            return Ok(auditoriumDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuditoriumDTO>> GetAuditorium(int id)
        {
            var auditoriumDTO = await _auditoriumService.GetAuditoriumByIdAsync(id);
            if (auditoriumDTO == null)
            {
                return NotFound();
            }
            return Ok(auditoriumDTO);
        }

        [HttpPost]
        public async Task<ActionResult<AuditoriumDTO>> CreateAuditorium([FromBody] AuditoriumDTO auditoriumDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdAuditoriumDTO = await _auditoriumService.CreateAuditoriumAsync(auditoriumDTO);
            return CreatedAtAction(nameof(GetAuditorium), new { id = createdAuditoriumDTO.Id }, createdAuditoriumDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuditorium(int id, [FromBody] AuditoriumDTO auditoriumDTO)
        {
            if (id != auditoriumDTO.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the body.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _auditoriumService.UpdateAuditoriumAsync(auditoriumDTO);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditorium(int id)
        {
            var result = await _auditoriumService.DeleteAuditoriumAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Decommentare e implementare se necessario
        // [HttpGet("cinema/{cinemaId}")]
        // public async Task<ActionResult<IEnumerable<AuditoriumDTO>>> GetAuditoriumsByCinema(int cinemaId)
        // {
        //     var auditoriumDTOs = await _auditoriumService.GetAuditoriumsByCinemaIdAsync(cinemaId);
        //     return Ok(auditoriumDTOs);
        // }
    }
}