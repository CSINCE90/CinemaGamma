using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaFrontend.Models;
using CinemaFrontend.Services.Movies;
using CinemaFrontend.Services.Auditoriums;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace CinemaFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IAuditoriumService _auditoriumService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMovieService movieService, IAuditoriumService auditoriumService, ILogger<HomeController> logger)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _auditoriumService = auditoriumService ?? throw new ArgumentNullException(nameof(auditoriumService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = new HomeViewModel
                {
                    FeaturedMovies = await _movieService.GetFeaturedMoviesAsync(),
                    UpcomingMovies = await _movieService.GetUpcomingMoviesAsync(),
                    AllMovies = await _movieService.GetAllMoviesAsync(),
                    AllAuditorium = await _auditoriumService.GetAllAuditoriumsAsync()

                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il caricamento della home page");
                return RedirectToAction(nameof(Error), new { errorMessage = "Si   verificato un errore durante il caricamento della home page." });
            }
        }

        public IActionResult IlCinema()
        {
            return View(Index);
        }
        [Authorize(Roles = "admin")]
        public IActionResult AdminOnly()
        {
            var roles = User.Claims.Where(c => c.Type == "roles" || c.Type == ClaimTypes.Role)
                            .Select(c => c.Value);
            _logger.LogInformation("User accessing AdminOnly. Roles: {Roles}", string.Join(", ", roles));
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult DebugClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Json(claims);
        }



    }
}
