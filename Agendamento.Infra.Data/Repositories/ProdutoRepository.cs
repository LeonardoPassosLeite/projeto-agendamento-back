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

        public async Task<Produto> GetByCategoriaAsync(int? id)
        {
            if (id == null)
                throw new ValidationException("Id não pode ser nulo.");

            try
            {
                var produto = await _produtoContext.Produtos.Include(p => p.Categoria)
                    .SingleOrDefaultAsync(p => p.Id == id);

                if (produto == null)
                    throw new NotFoundException($"Produto com Id {id} não encontrado.");

                if (produto.Categoria == null)
                    throw new NotFoundException($"Categoria para o Produto com Id {id} não encontrada.");


                return produto;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("An error occurred while retrieving the product by category.", ex);
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
                throw new DatabaseException("An error occurred while updating the product.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("An unexpected error occurred while updating the product.", ex);
            }
        }
    }
}