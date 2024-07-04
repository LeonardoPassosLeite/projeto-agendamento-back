using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> GetProdutosAsync();
        Task<ProdutoDTO> GetByIdAsync(int? id);
        Task<IEnumerable<ProdutoDTO>> GetByCategoriaIdAsync(int? id);
        Task<ProdutoDTO> CreateProdutoAsync(ProdutoDTO produtoDto);
        Task<ProdutoDTO> UpdateProdutoAsync(ProdutoDTO produtoDto);
        Task DisableProdutoAsync(ProdutoDTO produtoDto);
    }
}