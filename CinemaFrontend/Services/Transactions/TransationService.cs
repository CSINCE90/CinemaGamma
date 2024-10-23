using CinemaFrontend.DTO;
using CinemaFrontend.Data;
using Microsoft.EntityFrameworkCore;
using CinemaFrontend.Models;

namespace CinemaFrontend.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Reservation)
                .ToListAsync();
            return transactions.Select(MapToDTO);
        }

        public async Task<TransactionDTO> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Reservation)
                .FirstOrDefaultAsync(t => t.Id == id);
            return MapToDTO(transaction);
        }

        public async Task<TransactionDTO> CreateTransactionAsync(TransactionDTO transactionDto)
        {
            var transaction = MapToEntity(transactionDto);
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return MapToDTO(transaction);
        }

        public async Task<TransactionDTO> UpdateTransactionAsync(TransactionDTO transactionDto)
        {
            var transaction = await _context.Transactions.FindAsync(transactionDto.Id);
            if (transaction == null) return null;
            UpdateEntityFromDTO(transaction, transactionDto);
            await _context.SaveChangesAsync();
            return MapToDTO(transaction);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByReservationIdAsync(int reservationId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.ReservationId == reservationId)
                .ToListAsync();
            return transactions.Select(MapToDTO);
        }

        private TransactionDTO MapToDTO(Transaction transaction)
        {
            if (transaction == null) return null;
            return new TransactionDTO
            {
                Id = transaction.Id,
                ReservationId = transaction.ReservationId,
                // Map other properties as needed
                // For example:
                // Amount = transaction.Amount,
                // Date = transaction.Date,
                // etc.
            };
        }

        private Transaction MapToEntity(TransactionDTO dto)
        {
            if (dto == null) return null;
            return new Transaction
            {
                Id = dto.Id,
                ReservationId = dto.ReservationId,
                // Map other properties as needed
                // For example:
                // Amount = dto.Amount,
                // Date = dto.Date,
                // etc.
            };
        }

        private void UpdateEntityFromDTO(Transaction entity, TransactionDTO dto)
        {
            // Update properties as needed
            // For example:
            // entity.Amount = dto.Amount;
            // entity.Date = dto.Date;
            // etc.
            entity.ReservationId = dto.ReservationId;
        }
    }
}