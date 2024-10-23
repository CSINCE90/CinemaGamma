using System.ComponentModel.DataAnnotations;

namespace CinemaFrontend.DTO
{
    public class CinemaDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        
        
    }
}
