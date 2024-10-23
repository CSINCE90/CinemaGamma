
using CinemaBackend.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaBackend.Services
{
    public interface IAuditoriumService
    {
        Task<IEnumerable<AuditoriumDTO>> GetAllAuditoriumsAsync();
        Task<AuditoriumDTO> GetAuditoriumByIdAsync(int id);
        Task<AuditoriumDTO> CreateAuditoriumAsync(AuditoriumDTO auditoriumDTO);
        Task<bool> UpdateAuditoriumAsync(AuditoriumDTO auditoriumDTO);
        Task<bool> DeleteAuditoriumAsync(int id);
    }
}