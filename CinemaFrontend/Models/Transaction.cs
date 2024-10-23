using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaFrontend.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Required]
        public bool IsSuccessful { get; set; }

        // Relazione con Reservation
        public Reservation Reservation { get; set; }
    }
}

