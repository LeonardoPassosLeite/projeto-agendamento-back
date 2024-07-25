using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;

namespace Agendamento.Application.Interfaces
{
    public interface IProdutoService : IGenericService<ProdutoDTO>
    {
        Task AddFotoToProdutoAsync(int produtoId, FotoDTO fotoDto);
        Task<PagedResultDTO<ProdutoFotoDTO>> GetPagedProdutosAsync(PaginationParams paginationParams);
        Task UpdateStatusProdutoAsync(int id, bool isActive);
        Task<IEnumerable<ProdutoDTO>> GetProdutoByCategoriaIdAsync(int? id);
    }
}