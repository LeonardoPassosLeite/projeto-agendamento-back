using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        ApplicationDbContext _produtoContext;
        public ProdutoRepository(ApplicationDbContext context)
        {
            _produtoContext = context;
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            _produtoContext.Produtos.Add(produto);
            await _produtoContext.SaveChangesAsync();

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _produtoContext.Produtos.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int? id)
        {
            return await _produtoContext.Produtos.FindAsync(id);
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            _produtoContext.Update(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }

        public async Task DisableAsync(Produto produto)
        {
            _produtoContext.Produtos.Remove(produto);
            await _produtoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _produtoContext.Produtos
             .Where(p => p.CategoriaId == categoriaId)
             .Include(p => p.Categoria)
             .ToListAsync();
        }
    }
}