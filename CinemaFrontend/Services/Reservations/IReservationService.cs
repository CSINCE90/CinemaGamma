using CinemaFrontend.Models;
using CinemaFrontend.DTO;

namespace CinemaFrontend.Services.Reservations
{
    public interface IReservationService
    {
        Task<ReservationDTO> GetReservationByIdAsync(int id);
        Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync();
        Task<ReservationDTO> CreateReservationAsync(ReservationDTO reservationDto);
        Task<ReservationDTO> UpdateReservationAsync(ReservationDTO reservationDto);
        Task<bool> DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationDTO>> GetReservationsByUserIdAsync(int userId);
        Task<IEnumerable<ReservationDTO>> GetReservationsByMovieIdAsync(int movieId);
    }
}