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

        public ProdutoRepository(ApplicationDbContext prdutoContext) : base(prdutoContext)
        {
            _produtoContext = prdutoContext;
        }

        public override async Task<IEnumerable<Produto>> GetAllAsync(Expression<Func<Produto, object>>? orderBy = null)
        {
            var query = _produtoContext.Produtos.Include(p => p.FotoPrincipal).AsQueryable();
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            return await query.ToListAsync();
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