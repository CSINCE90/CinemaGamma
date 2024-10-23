using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaFrontend.Models
{
    [Table("Auditorium")]
    public class Auditorium
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cinema")]
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

        // Relazione con Cinema
        //public Cinema Cinema { get; set; }
    }
}
