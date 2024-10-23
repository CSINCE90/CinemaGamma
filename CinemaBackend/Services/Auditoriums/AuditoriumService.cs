
using CinemaBackend.Data;
using CinemaBackend.DTO;
using CinemaBackend.Models;
using Microsoft.EntityFrameworkCore;


namespace CinemaBackend.Services
{


    public class AuditoriumService : IAuditoriumService
    {
        private readonly ApplicationDbContext _context;

        public AuditoriumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditoriumDTO>> GetAllAuditoriumsAsync()
        {
            var auditoriums = await _context.Auditoriums.Include(a => a.CinemaId).ToListAsync();
            return auditoriums.Select(a => MapToDTO(a));
        }

        public async Task<AuditoriumDTO> GetAuditoriumByIdAsync(int id)
        {
            var auditorium = await _context.Auditoriums.Include(a => a.CinemaId).FirstOrDefaultAsync(a => a.Id == id);
            return auditorium != null ? MapToDTO(auditorium) : null;
        }

        public async Task<AuditoriumDTO> CreateAuditoriumAsync(AuditoriumDTO auditoriumDTO)
        {
            var auditorium = MapToEntity(auditoriumDTO);
            _context.Auditoriums.Add(auditorium);
            await _context.SaveChangesAsync();
            return MapToDTO(auditorium);
        }

        public async Task<bool> UpdateAuditoriumAsync(AuditoriumDTO auditoriumDTO)
        {
            var auditorium = await _context.Auditoriums.FindAsync(auditoriumDTO.Id);
            if (auditorium == null)
            {
                return false;
            }

            UpdateEntityFromDTO(auditorium, auditoriumDTO);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAuditoriumAsync(int id)
        {
            var auditorium = await _context.Auditoriums.FindAsync(id);
            if (auditorium == null)
            {
                return false;
            }

            _context.Auditoriums.Remove(auditorium);
            await _context.SaveChangesAsync();
            return true;
        }

        private AuditoriumDTO MapToDTO(Auditorium auditorium)
        {
            return new AuditoriumDTO
            {
                Id = auditorium.Id,
                CinemaId = auditorium.CinemaId,
                Name = auditorium.Name,
                Capacity = auditorium.Capacity,
                Is3D = auditorium.Is3D,
                IsIMAX = auditorium.IsIMAX,
                IsDolbyAtmos = auditorium.IsDolbyAtmos,
                //CinemaName = auditorium.Cinema?.Name
            };
        }

        private Auditorium MapToEntity(AuditoriumDTO dto)
        {
            if (dto == null) return null;
            return new Auditorium
            {
                Id = dto.Id,
                CinemaId = dto.CinemaId,
                Name = dto.Name,
                Capacity = dto.Capacity,
                Is3D = dto.Is3D,
                IsIMAX = dto.IsIMAX,
                IsDolbyAtmos = dto.IsDolbyAtmos
            };
        }

        private void UpdateEntityFromDTO(Auditorium entity, AuditoriumDTO dto)
        {
            entity.CinemaId = dto.CinemaId;
            entity.Name = dto.Name;
            entity.Capacity = dto.Capacity;
            entity.Is3D = dto.Is3D;
            entity.IsIMAX = dto.IsIMAX;
            entity.IsDolbyAtmos = dto.IsDolbyAtmos;
        }
    }
}
