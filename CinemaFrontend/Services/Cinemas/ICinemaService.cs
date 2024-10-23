using CinemaFrontend.Models;
using CinemaFrontend.DTO;

namespace CinemaFrontend.Services.Cinemas
        
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