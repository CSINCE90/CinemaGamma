
using CinemaFrontend.DTO;
using System.ComponentModel.DataAnnotations;

namespace CinemaFrontend.Models
{
    public class HomeViewModel
    {
        [Required]
        public IEnumerable<MovieDTO> FeaturedMovies { get; set; }
        public IEnumerable<MovieDTO> UpcomingMovies { get; set; }
        public IEnumerable<AuditoriumDTO> AllAuditorium { get; set; }
        public IEnumerable<MovieDTO> AllMovies { get; set; }


        //public IEnumerable<ReviewDTO> LatestReviews { get; set; }
    }
}
