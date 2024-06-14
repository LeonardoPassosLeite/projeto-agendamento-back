using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task Disable(Categoria categoria);
    }
}