using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IProdutoService : ICustomService<ProdutoDTO, ProdutoFotoDTO>
    {
        Task UpdateStatusProdutoAsync(int id, bool isActive);
        Task<IEnumerable<ProdutoDTO>> GetProdutosByCategoriaIdAsync(int? id);
    }
}