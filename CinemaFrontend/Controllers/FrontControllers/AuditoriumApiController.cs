using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using CinemaFrontend.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaFrontend.Controllers.FrontControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriumApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _backendUrl = "http://localhost:5166/api/AuditoriumApi";

        public AuditoriumApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditoriumDTO>>> GetAuditoriums()
        {
            var auditoriums = await _httpClient.GetFromJsonAsync<IEnumerable<AuditoriumDTO>>(_backendUrl);
            return Ok(auditoriums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuditoriumDTO>> GetAuditorium(int id)
        {
            var auditorium = await _httpClient.GetFromJsonAsync<AuditoriumDTO>($"{_backendUrl}/{id}");
            if (auditorium == null)
            {
                return NotFound();
            }
            return Ok(auditorium);
        }

        [HttpPost]
        public async Task<ActionResult<AuditoriumDTO>> PostAuditorium(AuditoriumDTO auditoriumDto)
        {
            var response = await _httpClient.PostAsJsonAsync(_backendUrl, auditoriumDto);
            if (response.IsSuccessStatusCode)
            {
                var createdAuditorium = await response.Content.ReadFromJsonAsync<AuditoriumDTO>();
                return CreatedAtAction(nameof(GetAuditorium), new { id = createdAuditorium.Id }, createdAuditorium);
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuditorium(int id, AuditoriumDTO auditoriumDto)
        {
            if (id != auditoriumDto.Id)
            {
                return BadRequest();
            }
            var response = await _httpClient.PutAsJsonAsync($"{_backendUrl}/{id}", auditoriumDto);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditorium(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_backendUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            return StatusCode((int)response.StatusCode);
        }
    }
}