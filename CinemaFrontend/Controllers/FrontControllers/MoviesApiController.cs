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
    public class MoviesApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _backendUrl = "http://localhost:5166/api/MoviesApi";

        public MoviesApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/MoviesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await _httpClient.GetFromJsonAsync<IEnumerable<MovieDTO>>(_backendUrl);
            return Ok(movies);
        }

        // GET: api/MoviesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var movie = await _httpClient.GetFromJsonAsync<MovieDTO>($"{_backendUrl}/{id}");
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // PUT: api/MoviesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDTO movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest();
            }
            var response = await _httpClient.PutAsJsonAsync($"{_backendUrl}/{id}", movieDto);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            return NoContent();
        }

        // POST: api/MoviesApi
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MovieDTO movieDto)
        {
            var response = await _httpClient.PostAsJsonAsync(_backendUrl, movieDto);
            if (response.IsSuccessStatusCode)
            {
                var createdMovie = await response.Content.ReadFromJsonAsync<MovieDTO>();
                return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
            }
            return StatusCode((int)response.StatusCode);
        }

        // DELETE: api/MoviesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
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