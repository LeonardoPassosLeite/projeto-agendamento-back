using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        Task DisableAsync(Produto produto);
        Task<IEnumerable<Produto>> GetByCategoriaIdAsync(int categoriaId);
    }
}