using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IFotoService
    {
        Task<FotoDTO> UploadFileAsync(FotoUploadDTO  fotoUploadDto);
    }
}