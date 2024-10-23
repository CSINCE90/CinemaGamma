
using CinemaFrontend.Models;
using CinemaFrontend.DTO;
using Microsoft.EntityFrameworkCore;

namespace CinemaFrontend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
      
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CinemaFrontend.DTO.MovieDTO> MovieDTO { get; set; } = default!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
        //public DbSet<CinemaBackend.DTO.MovieDTO> MovieDTO { get; set; } = default!;
    }
}