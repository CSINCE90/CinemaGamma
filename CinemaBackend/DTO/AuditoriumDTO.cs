using System.ComponentModel.DataAnnotations;

namespace CinemaBackend.DTO
{
    public class AuditoriumDTO
    {
        public int Id { get; set; }

        [Required]
        public int CinemaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public bool Is3D { get; set; }

        [Required]
        public bool IsIMAX { get; set; }

        [Required]
        public bool IsDolbyAtmos { get; set; }

       
    }
}
