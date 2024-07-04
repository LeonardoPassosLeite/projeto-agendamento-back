using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infra.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        ApplicationDbContext _categoriaContext;
        public CategoriaRepository(ApplicationDbContext context)
        {
            _categoriaContext = context;
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            _categoriaContext.Categorias.Add(categoria);
            await _categoriaContext.SaveChangesAsync();

            return categoria;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _categoriaContext.Categorias.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int? id)
        {
            return await _categoriaContext.Categorias.FindAsync(id);
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            _categoriaContext.Update(categoria);
            await _categoriaContext.SaveChangesAsync();
            return categoria;
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _categoriaContext.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _categoriaContext.Categorias.Remove(categoria);
                await _categoriaContext.SaveChangesAsync();
            }
        }

        public async Task Disable(Categoria categoria)
        {
            _categoriaContext.Categorias.Update(categoria);
            await _categoriaContext.SaveChangesAsync();
        }
    }
}