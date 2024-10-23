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
    public class ReservationApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _backendUrl = "http://localhost:5166/api/ReservationApi";

        public ReservationApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = await _httpClient.GetFromJsonAsync<IEnumerable<ReservationDTO>>(_backendUrl);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            var reservation = await _httpClient.GetFromJsonAsync<ReservationDTO>($"{_backendUrl}/{id}");
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDto)
        {
            var response = await _httpClient.PostAsJsonAsync(_backendUrl, reservationDto);
            if (response.IsSuccessStatusCode)
            {
                var createdReservation = await response.Content.ReadFromJsonAsync<ReservationDTO>();
                return CreatedAtAction(nameof(GetReservation), new { id = createdReservation.Id }, createdReservation);
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservationDto)
        {
            if (id != reservationDto.Id)
            {
                return BadRequest();
            }
            var response = await _httpClient.PutAsJsonAsync($"{_backendUrl}/{id}", reservationDto);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
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