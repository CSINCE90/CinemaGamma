
using Microsoft.AspNetCore.Mvc;
using CinemaFrontend.DTO;
using CinemaFrontend.Services.Movies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CinemaFrontend.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return View(movies);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movie/Create
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(MovieDTO movieDTO)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddMovieAsync(movieDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(movieDTO);
        }

        // GET: Movie/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, MovieDTO movieDTO)
        {
            if (id != movieDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedMovie = await _movieService.UpdateMovieAsync(movieDTO);
                if (updatedMovie == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieDTO);
        }

        // GET: Movie/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Movie/Featured
        public async Task<IActionResult> Featured()
        {
            var featuredMovies = await _movieService.GetFeaturedMoviesAsync();
            return View(featuredMovies);
        }

        // GET: Movie/Upcoming
        public async Task<IActionResult> Upcoming()
        {
            var upcomingMovies = await _movieService.GetUpcomingMoviesAsync();
            return View(upcomingMovies);
        }

        // GET: Movie/Genre/{genre}
        public async Task<IActionResult> Genre(string genre)
        {
            var movies = await _movieService.GetMoviesByGenreAsync(genre);
            ViewData["Genre"] = genre;
            return View(movies);
        }
    }
}