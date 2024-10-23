using CinemaFrontend.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaFrontend.Services.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO> GetTransactionByIdAsync(int id);
        Task<TransactionDTO> CreateTransactionAsync(TransactionDTO transactionDto);
        Task<TransactionDTO> UpdateTransactionAsync(TransactionDTO transactionDto);
        Task<bool> DeleteTransactionAsync(int id);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByReservationIdAsync(int reservationId);
    }
}