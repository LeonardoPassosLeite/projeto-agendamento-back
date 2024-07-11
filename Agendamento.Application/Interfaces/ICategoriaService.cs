using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface ICategoriaService : IGenericService<CategoriaDTO, CategoriaUpdateDTO>
    {
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();
        Task UpdateStatusAsync(int id, bool isActive);
    }
}