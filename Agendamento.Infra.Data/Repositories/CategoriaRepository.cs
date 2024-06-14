using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
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

        public async Task<Categoria> Create(Categoria entity)
        {
            if (entity == null)
                throw new ValidationException("Categoria não pode ser nulo.");


            _categoriaContext.Categorias.Add(entity);
            await _categoriaContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(int id)
        {
            var categoria = await _categoriaContext.Categorias.FindAsync(id);

            if (categoria == null)
                throw new NotFoundException($"Categoria com Id {id} não encontrado.");


            _categoriaContext.Categorias.Remove(categoria);
            await _categoriaContext.SaveChangesAsync();
        }

        public async Task Disable(Categoria categoria)
        {
            var existeCategoria = await _categoriaContext.Categorias.FindAsync(categoria.Id);

            if (existeCategoria == null)
                throw new NotFoundException($"Categoria com Id {categoria.Id} não encontrado.");


            existeCategoria.Disable();

            _categoriaContext.Categorias.Update(existeCategoria);
            await _categoriaContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            try
            {
                return await _categoriaContext.Categorias.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Um erro aconteceu ao buscar as categorias.", ex);
            }
        }

        public async Task<Categoria> GetById(int? id)
        {
            if (id == null)
                throw new ValidationException("Id não pode ser nulo");

            var categoria = await _categoriaContext.Categorias.FindAsync(id);

            if (categoria == null)
                throw new NotFoundException($"Categoria com Id {id} não encontrado");

            return categoria;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ValidationException("Categoria não pode ser nulo");
            }

            try
            {
                _categoriaContext.Update(categoria);
                await _categoriaContext.SaveChangesAsync();
                return categoria;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("Um erro ocorreu ao atualizar a categoria.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Um erro inesperado ocorreu ao atualizar a categoria.", ex);
            }
        }
    }
}