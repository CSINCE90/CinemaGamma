
using Microsoft.AspNetCore.Mvc;
using CinemaBackend.Data;
using CinemaBackend.DTO;
using CinemaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransactionsApi
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();
            var transactionDTOs = transactions.Select(t => new TransactionDTO
            {
                Id = t.Id,
                // Map other properties as needed
            }).ToList();
            return Ok(transactionDTOs);
        }

        // GET: api/TransactionsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            var transactionDTO = new TransactionDTO
            {
                Id = transaction.Id,
                // Map other properties as needed
            };
            return Ok(transactionDTO);
        }

        // POST: api/TransactionsApi
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO transactionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transaction = new Transaction
            {
                // Map properties from transactionDTO to transaction
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            transactionDTO.Id = transaction.Id;
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transactionDTO);
        }

        // PUT: api/TransactionsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDTO transactionDTO)
        {
            if (id != transactionDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            // Update transaction properties from transactionDTO
            // For example: transaction.PropertyName = transactionDTO.PropertyName;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TransactionsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}