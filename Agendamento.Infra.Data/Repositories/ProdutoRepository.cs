using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
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

        public async Task<Produto> CreateAsync(Produto produto)
        {
            if (produto == null)
                throw new ValidationException("Produto não pode ser nulo.");


            _produtoContext.Produtos.Add(produto);
            await _produtoContext.SaveChangesAsync();

            return produto;
        }

        public async Task DisableAsync(Produto produto)
        {
            var existeProduto = await _produtoContext.Produtos.FindAsync(produto.Id);

            if (existeProduto == null)
                throw new NotFoundException($"Produto com Id {produto.Id} não encontrado.");


            existeProduto.Disable();

            _produtoContext.Produtos.Update(existeProduto);
            await _produtoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            try
            {
                return await _produtoContext.Produtos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Um erro aconteceu ao buscar os produtos.", ex);
            }
        }

        public async Task<Produto> GetByIdAsync(int? id)
        {
            if (id == null)
                throw new ValidationException("Id não pode ser nulo.");

            var produto = await _produtoContext.Produtos.FindAsync(id);

            if (produto == null)
                throw new NotFoundException($"Produto com o Id {id} não encontrado.");

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaIdAsync(int categoriaId)
        {
            try
            {
                var produtos = await _produtoContext.Produtos
                    .Where(p => p.CategoriaId == categoriaId)
                    .Include(p => p.Categoria)
                    .ToListAsync();

                if (!produtos.Any())
                {
                    throw new NotFoundException($"Nenhum produto encontrado para a categoria com Id {categoriaId}.");
                }

                return produtos;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Um erro ocorreu ao buscar os produtos pela categoria.", ex);
            }
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            if (produto == null)
            {
                throw new ValidationException("Produto não pode ser nulo.");
            }

            try
            {
                _produtoContext.Update(produto);
                await _produtoContext.SaveChangesAsync();
                return produto;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("Ocorreu um erro ao atualizar o produto.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Ocorreu um erro inesperado ao atualizar o produto.", ex);
            }
        }
    }
}