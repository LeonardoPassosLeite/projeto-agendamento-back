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

        public async Task<(IEnumerable<Produto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? filterText)
        {
            IQueryable<Produto> query = _produtoContext.Produtos
                .Include(p => p.FotoPrincipal);

            if (!string.IsNullOrWhiteSpace(filterText))
                query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "Nome").ToLower(), $"%{filterText.ToLower()}%"));

            int totalCount = await query.CountAsync();

            var produtos = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (produtos, totalCount);
        }

        public override async Task<Produto?> GetByIdAsync(int id)
        {
            return await _produtoContext.Produtos
                    .Include(p => p.FotoPrincipal)
                    .FirstOrDefaultAsync(p => p.Id == id);
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