using CinemaBackend.Models;
using CinemaBackend.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CinemaBackend.Data;

namespace CinemaBackend.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieDTO>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<MovieDTO> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return null;

            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                TrailerUrl = movie.TrailerUrl,
                ImageUrl = movie.ImageUrl
            };
        }

        public async Task<MovieDTO> AddMovieAsync(MovieDTO movieDto)
        {
            var movie = new Models.Movie // Usa il nome completo se necessario
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate,
                Genre = movieDto.Genre,
                TrailerUrl = movieDto.TrailerUrl,
                ImageUrl = movieDto.ImageUrl
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            movieDto.Id = movie.Id;
            return movieDto;
        }

        public async Task<MovieDTO> UpdateMovieAsync(MovieDTO movieDto)
        {
            var movie = await _context.Movies.FindAsync(movieDto.Id);
            if (movie == null)
                return null;

            movie.Title = movieDto.Title;
            movie.Description = movieDto.Description;
            movie.ReleaseDate = movieDto.ReleaseDate;
            movie.Genre = movieDto.Genre;
            movie.TrailerUrl = movieDto.TrailerUrl;
            movie.ImageUrl = movieDto.ImageUrl;

            await _context.SaveChangesAsync();
            return movieDto;
        }

        public async Task<MovieDTO> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return null;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                TrailerUrl = movie.TrailerUrl,
                ImageUrl = movie.ImageUrl
            };
        }
        public async Task<IEnumerable<MovieDTO>> GetMoviesByGenreAsync(string genre)
        {
            return await _context.Movies
                .Where(m => m.Genre.ToLower() == genre.ToLower())
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovieDTO>> SearchMoviesAsync(string searchTerm)
        {
            return await _context.Movies
                .Where(m => m.Title.Contains(searchTerm) || m.Description.Contains(searchTerm))
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }


        //COMMENTATO PERCHE' HO TOLTO I REVIEW
        //public async Task<IEnumerable<MovieDTO>> GetMoviesWithReviewsAsync()
        //{
        //    var movies = await _context.Movies
        //        .Include(m => m.Reviews)
        //        .Select(m => new MovieDTO
        //        {
        //            Id = m.Id,
        //            Title = m.Title,
        //            Description = m.Description,
        //            ReleaseDate = m.ReleaseDate,
        //            Genre = m.Genre,
        //            TrailerUrl = m.TrailerUrl,
        //            ImageUrl = m.ImageUrl,
                    // Assumendo che MovieDTO abbia una proprietà Reviews
                    //Reviews = m.Reviews.Select(r => new ReviewDTO
                    //{
                    //    Id = r.Id,
                    //    Content = r.Content,
                    //    Rating = r.Rating,
                    //    // ... altre proprietà di ReviewDTO ...
                    //}).ToList()
        //        })
        //        .ToListAsync();

        //    return movies;
        //}

        //public async Task<MovieDTO> GetMovieWithReviewsAsync(int id)
        //{
        //    var movie = await _context.Movies
        //        .Include(m => m.Reviews)
        //        .Where(m => m.Id == id)
        //        .Select(m => new MovieDTO
        //        {
        //            Id = m.Id,
        //            Title = m.Title,
        //            Description = m.Description,
        //            ReleaseDate = m.ReleaseDate,
        //            Genre = m.Genre,
        //            TrailerUrl = m.TrailerUrl,
        //            ImageUrl = m.ImageUrl,
                    // Assumendo che MovieDTO abbia una proprietà Reviews
                    //Reviews = m.Reviews.Select(r => new ReviewDTO
                    //{
                    //    Id = r.Id,
                    //    Content = r.Content,
                    //    Rating = r.Rating,
                    //    // ... altre proprietà di ReviewDTO ...
                    //}).ToList()
        //        })
        //        .FirstOrDefaultAsync();

        //    return movie;
        //}
        public async Task<IEnumerable<MovieDTO>> GetFeaturedMoviesAsync(int count)
        {
            return await _context.Movies
                .OrderByDescending(m => m.ReleaseDate)
                .Take(count)
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MovieDTO>> GetUpcomingMoviesAsync(int count)
        {
            return await _context.Movies
                .OrderBy(m => m.ReleaseDate)
                .Take(count)
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }
        // Implementa GetFeaturedMoviesAsync
        public async Task<IEnumerable<MovieDTO>> GetFeaturedMoviesAsync()
        {
            return await _context.Movies
                .Where(m => m.IsFeatured)
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        // Correggi GetUpcomingMoviesAsync
        public async Task<IEnumerable<MovieDTO>> GetUpcomingMoviesAsync()
        {
            var today = DateTime.Today;
            return await _context.Movies
                .Where(m => m.ReleaseDate > today)
                .OrderBy(m => m.ReleaseDate)
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    TrailerUrl = m.TrailerUrl,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }


    }
}
