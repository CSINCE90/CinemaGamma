using Microsoft.EntityFrameworkCore;
using CinemaBackend.Data;
using CinemaBackend.DTO;
using CinemaBackend.Models;


namespace CinemaBackend.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly ApplicationDbContext _context;

        public CinemaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CinemaDTO>> GetAllCinemasAsync()
        {
            var cinemas = await _context.Cinemas
                //.Include(c => c.Auditoriums)
                .ToListAsync();
            return cinemas.Select(MapToDTO);
        }

        public async Task<CinemaDTO> GetCinemaByIdAsync(int id)
        {
            var cinema = await _context.Cinemas
                //.Include(c => c.Auditoriums)
                .FirstOrDefaultAsync(c => c.Id == id);
            return MapToDTO(cinema);
        }

        public async Task<CinemaDTO> CreateCinemaAsync(CinemaDTO cinemaDto)
        {
            var cinema = MapToEntity(cinemaDto);
            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();
            return MapToDTO(cinema);
        }

        public async Task<bool> UpdateCinemaAsync(CinemaDTO cinemaDto)
        {
            var cinema = await _context.Cinemas.FindAsync(cinemaDto.Id);
            if (cinema == null)
                return false;
            UpdateEntityFromDTO(cinema, cinemaDto);
            _context.Entry(cinema).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(cinemaDto.Id))
                    return false;
                else
                    throw;
            }
            return true;
        }

        public async Task<bool> DeleteCinemaAsync(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            if (cinema == null)
                return false;
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool CinemaExists(int id)
        {
            return _context.Cinemas.Any(e => e.Id == id);
        }

        private CinemaDTO MapToDTO(Cinema cinema)
        {
            if (cinema == null) return null;
            return new CinemaDTO
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Address = cinema.Address,
                // Map other properties as needed
                //Auditoriums = cinema.Auditoriums?.Select(a => new AuditoriumDTO
                //{
                //    Id = a.Id,
                //    Name = a.Name,
                //    Capacity = a.Capacity,
                //    // Map other Auditorium properties as needed
                //}).ToList()
            };
        }

        private Cinema MapToEntity(CinemaDTO dto)
        {
            if (dto == null) return null;
            return new Cinema
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                // Map other properties as needed
                // Note: We typically don't map Auditoriums here to avoid unintended creation/updates
            };
        }

        private void UpdateEntityFromDTO(Cinema entity, CinemaDTO dto)
        {
            entity.Name = dto.Name;
            entity.Address = dto.Address;
            // Update other properties as needed
            // Note: Be cautious about updating related entities (like Auditoriums) here
        }
    }
}