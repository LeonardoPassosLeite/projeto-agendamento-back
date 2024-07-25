using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        Task DisableAsync(Produto produto);
        Task<(IEnumerable<Produto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? filterText);
        Task<IEnumerable<Produto>> GetByCategoriaIdAsync(int categoriaId);

    }
}