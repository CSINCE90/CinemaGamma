using CinemaFrontend.DTO;
using CinemaFrontend.Data;
using Microsoft.EntityFrameworkCore;
using CinemaFrontend.Models;


namespace CinemaFrontend.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                //.Include(r => r.User)
                .Include(r => r.Movie)
                .Include(r => r.Auditorium)
                .FirstOrDefaultAsync(r => r.Id == id);

            return MapToDTO(reservation);
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync()
        {
            var reservations = await _context.Reservations
                //.Include(r => r.User)
                .Include(r => r.Movie)
                .Include(r => r.Auditorium)
                .ToListAsync();

            return reservations.Select(MapToDTO);
        }

        public async Task<ReservationDTO> CreateReservationAsync(ReservationDTO reservationDto)
        {
            var reservation = MapToEntity(reservationDto);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return MapToDTO(reservation);
        }

        public async Task<ReservationDTO> UpdateReservationAsync(ReservationDTO reservationDto)
        {
            var reservation = await _context.Reservations.FindAsync(reservationDto.Id);
            if (reservation == null) return null;

            // Update properties
            reservation.UserId = reservationDto.UserId;
            reservation.MovieId = reservationDto.MovieId;
            reservation.AuditoriumId = reservationDto.AuditoriumId;
            reservation.ShowTime = reservationDto.ShowTime;
            reservation.SeatsReserved = reservationDto.SeatsReserved;
            reservation.ReservationDate = reservationDto.ReservationDate;

            await _context.SaveChangesAsync();
            return MapToDTO(reservation);
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsByUserIdAsync(int userId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Movie)
                .Include(r => r.Auditorium)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return reservations.Select(MapToDTO);
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsByMovieIdAsync(int movieId)
        {
            var reservations = await _context.Reservations
               //.Include(r => r.User)
                .Include(r => r.Auditorium)
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            return reservations.Select(MapToDTO);
        }

        private ReservationDTO MapToDTO(Reservation reservation)
        {
            return new ReservationDTO
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                MovieId = reservation.MovieId,
                AuditoriumId = reservation.AuditoriumId,
                ShowTime = reservation.ShowTime,
                SeatsReserved = reservation.SeatsReserved,
                ReservationDate = reservation.ReservationDate,
                //User = reservation.User != null ? new UserDTO { Id = reservation.User.Id, /* map other properties */ } : null,
                //Movie = reservation.Movie != null ? new MovieDTO { Id = reservation.Movie.Id, /* map other properties */ } : null,
                //Auditorium = reservation.Auditorium != null ? new AuditoriumDTO { Id = reservation.Auditorium.Id, /* map other properties */ } : null
            };
        }

        private Reservation MapToEntity(ReservationDTO dto)
        {
            return new Reservation
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MovieId = dto.MovieId,
                AuditoriumId = dto.AuditoriumId,
                ShowTime = dto.ShowTime,
                SeatsReserved = dto.SeatsReserved,
                ReservationDate = dto.ReservationDate
            };
        }
    }
}
