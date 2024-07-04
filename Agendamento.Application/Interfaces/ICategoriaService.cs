using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface ICategoriaService : IService<CategoriaDTO>
    {
        Task UpdateStatusAsync(int id, bool isActive);
        Task<CategoriaDTO> UpdateAsync(UpdateCategoriaDTO updateCategoriaDTO);
    }
}