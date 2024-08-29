using System.Linq.Expressions;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infra.Data.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        private readonly ApplicationDbContext _produtoContext;

        public ProdutoRepository(ApplicationDbContext produtoContext) : base(produtoContext)
        {
            _produtoContext = produtoContext;
        }

        public override async Task<Produto?> GetByIdAsync(int id, params Expression<Func<Produto, object>>[] includeProperties)
        {
            IQueryable<Produto> query = _produtoContext.Produtos;

            query = query.Include(p => p.Categoria)
                         .Include(p => p.FotoPrincipal);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
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
             .Include(p => p.FotoPrincipal)
             .ToListAsync();
        }
    }
}