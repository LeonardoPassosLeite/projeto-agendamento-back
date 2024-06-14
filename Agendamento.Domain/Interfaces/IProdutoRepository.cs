using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<Produto> GetById(int? id);
        Task<Produto> Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task Disable(Produto produto);
    }
}