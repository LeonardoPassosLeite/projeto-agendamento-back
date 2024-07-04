using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> AddAsync(Produto produto);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int? id);
        Task<Produto> UpdateAsync(Produto produto);
        Task DisableAsync(Produto produto);
        Task<IEnumerable<Produto>> GetByCategoriaIdAsync(int categoriaId);
    }
}