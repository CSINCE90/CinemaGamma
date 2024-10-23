using CinemaFrontend.Models;
using CinemaFrontend.DTO;
    
namespace CinemaFrontend.Services.Movies
{

    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO> GetMovieByIdAsync(int id);
        Task<MovieDTO> AddMovieAsync(MovieDTO movieDto);
        Task<MovieDTO> UpdateMovieAsync(MovieDTO movieDto);
        Task<MovieDTO> DeleteMovieAsync(int id);
        //Task<IEnumerable<MovieDTO>> GetMoviesWithReviewsAsync();
        //Task<MovieDTO> GetMovieWithReviewsAsync(int id);
        Task<IEnumerable<MovieDTO>> GetFeaturedMoviesAsync(int count);
        Task<IEnumerable<MovieDTO>> GetUpcomingMoviesAsync(int count);
        Task<IEnumerable<MovieDTO>> GetUpcomingMoviesAsync();
        Task<IEnumerable<MovieDTO>> GetFeaturedMoviesAsync();
        Task<IEnumerable<MovieDTO>> GetMoviesByGenreAsync(string genre);
    }
}
