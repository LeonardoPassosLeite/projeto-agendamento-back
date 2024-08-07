using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces.Commons;

namespace Agendamento.Application.Interfaces
{
    public interface IProdutoService : ICustomService<ProdutoDTO, ProdutoFotoDTO>
    {
        Task AddFotoToProdutoAsync(int produtoId, FotoDTO fotoDto);
        Task UpdateStatusProdutoAsync(int id, bool isActive);
        Task<IEnumerable<ProdutoDTO>> GetProdutosByCategoriaIdAsync(int? id);
    }
}
