using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task Disable(Categoria categoria);
    }
}