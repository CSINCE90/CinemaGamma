using CinemaFrontend.Data;
using CinemaFrontend.DTO;
using CinemaFrontend.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaFrontend.Services.Auditoriums
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
            return await _context.Auditoriums
                .Select(a => MapToDTO(a))
                .ToListAsync();
        }

        public async Task<AuditoriumDTO> GetAuditoriumByIdAsync(int id)
        {
            var auditorium = await _context.Auditoriums
                .FirstOrDefaultAsync(a => a.Id == id);
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

        private static AuditoriumDTO MapToDTO(Auditorium auditorium)
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
            };
        }

        private static Auditorium MapToEntity(AuditoriumDTO dto)
        {
            return dto == null ? null : new Auditorium
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

        private static void UpdateEntityFromDTO(Auditorium entity, AuditoriumDTO dto)
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
