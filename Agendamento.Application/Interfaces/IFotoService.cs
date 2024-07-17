using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IFotoService
    {
        Task<FotoDTO> AddFotoAsync(FotoDTO fotoDto);
        Task<FotoDTO> UpdateFotoAsync(FotoDTO fotoDto);
        Task<FotoDTO> GetFotoByIdAsync(int id);
    }
}
