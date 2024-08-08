using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface ICategoriaService : IGenericService<CategoriaDTO>
    {
        Task UpdateStatusCategoriaAsync(int id, bool isActive);
    }
}