using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto> GetByIdAsync(int? id);
        Task<IEnumerable<Produto>> GetByCategoriaIdAsync(int categoriaId);
        Task<Produto> CreateAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task DisableAsync(Produto produto);
    }
}