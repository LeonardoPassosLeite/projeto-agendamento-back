using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IFotoRepository : IGenericRepository<Foto>
    {
        Task<bool> FotoPrincipalExisteAsync(int produtoId);
    }
}