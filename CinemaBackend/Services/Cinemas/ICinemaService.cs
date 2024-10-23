using CinemaBackend.Models;
using CinemaBackend.DTO;

namespace CinemaBackend.Services

{
    public interface ICinemaService
    {
        Task<IEnumerable<CinemaDTO>> GetAllCinemasAsync();
        Task<CinemaDTO> GetCinemaByIdAsync(int id);
        Task<CinemaDTO> CreateCinemaAsync(CinemaDTO cinemaDto);
        Task<bool> UpdateCinemaAsync(CinemaDTO cinemaDto);
        Task<bool> DeleteCinemaAsync(int id);
    }
}