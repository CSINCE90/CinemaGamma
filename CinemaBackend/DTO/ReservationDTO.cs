using System.ComponentModel.DataAnnotations;



namespace CinemaBackend.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public int AuditoriumId { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }

        [Required]
        public int SeatsReserved { get; set; }

        public DateTime ReservationDate { get; set; }

        // Relazioni opzionali
        
        public MovieDTO Movie { get; set; }
        public AuditoriumDTO Auditorium { get; set; }
    }
}
