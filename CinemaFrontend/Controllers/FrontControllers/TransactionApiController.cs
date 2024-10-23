
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
    public class TransactionApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _backendUrl = "http://localhost:5166/api/TransactionApi";

        public TransactionApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            var transactions = await _httpClient.GetFromJsonAsync<IEnumerable<TransactionDTO>>(_backendUrl);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            var transaction = await _httpClient.GetFromJsonAsync<TransactionDTO>($"{_backendUrl}/{id}");
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> PostTransaction(TransactionDTO transactionDto)
        {
            var response = await _httpClient.PostAsJsonAsync(_backendUrl, transactionDto);
            if (response.IsSuccessStatusCode)
            {
                var createdTransaction = await response.Content.ReadFromJsonAsync<TransactionDTO>();
                return CreatedAtAction(nameof(GetTransaction), new { id = createdTransaction.Id }, createdTransaction);
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionDTO transactionDto)
        {
            if (id != transactionDto.Id)
            {
                return BadRequest();
            }
            var response = await _httpClient.PutAsJsonAsync($"{_backendUrl}/{id}", transactionDto);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
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