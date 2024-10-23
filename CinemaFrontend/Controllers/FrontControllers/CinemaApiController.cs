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
    public class CinemaApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _backendUrl = "http://localhost:5166/api/CinemaApi";

        public CinemaApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CinemaDTO>>> GetCinemas()
        {
            var cinemas = await _httpClient.GetFromJsonAsync<IEnumerable<CinemaDTO>>(_backendUrl);
            return Ok(cinemas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CinemaDTO>> GetCinema(int id)
        {
            var cinema = await _httpClient.GetFromJsonAsync<CinemaDTO>($"{_backendUrl}/{id}");
            if (cinema == null)
            {
                return NotFound();
            }
            return Ok(cinema);
        }

        [HttpPost]
        public async Task<ActionResult<CinemaDTO>> PostCinema(CinemaDTO cinemaDto)
        {
            var response = await _httpClient.PostAsJsonAsync(_backendUrl, cinemaDto);
            if (response.IsSuccessStatusCode)
            {
                var createdCinema = await response.Content.ReadFromJsonAsync<CinemaDTO>();
                return CreatedAtAction(nameof(GetCinema), new { id = createdCinema.Id }, createdCinema);
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinema(int id, CinemaDTO cinemaDto)
        {
            if (id != cinemaDto.Id)
            {
                return BadRequest();
            }
            var response = await _httpClient.PutAsJsonAsync($"{_backendUrl}/{id}", cinemaDto);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
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