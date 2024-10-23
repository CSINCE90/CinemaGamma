
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace CinemaFrontend.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [Required]
        [ForeignKey("Auditorium")]
        public int AuditoriumId { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }

        [Required]
        public int SeatsReserved { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.Now;

        //// Relazioni

        public Movie Movie { get; set; }
        public Auditorium Auditorium { get; set; }
    }
}
