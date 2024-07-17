using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IProdutoService : IGenericService<ProdutoDTO>
    {
        Task PublishProductAsync(int produtoId);
        Task AddFotoToProdutoAsync(int produtoId, FotoDTO fotoDto);
        Task UpdateStatusProdutoAsync(int id, bool isActive);
        Task<IEnumerable<ProdutoDTO>> GetProdutoByCategoriaIdAsync(int? id);
    }
}