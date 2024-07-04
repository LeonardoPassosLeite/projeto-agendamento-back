using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDTO> AddProdutoAsync(ProdutoActiveDTO produtoDto);
        Task<ProdutoDTO> GetProdutoByIdAsync(int? id);
        Task<IEnumerable<ProdutoDTO>> GetProdutosAsync();
        Task<ProdutoDTO> UpdateProdutoAsync(ProdutoActiveDTO produtoDto);
        Task<IEnumerable<ProdutoDTO>> GetProdutoByCategoriaIdAsync(int? id);
        Task UpdateStatusProdutoAsync(int id, bool isActive);
    }
}