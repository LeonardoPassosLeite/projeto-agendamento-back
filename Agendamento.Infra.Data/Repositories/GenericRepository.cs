using System.Linq.Expressions;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? orderBy = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (orderBy != null)
                query = query.OrderBy(orderBy);

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> UpdateAsync(T? entity)
        {
            if (entity == null)
                return null;

            var keyName = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(x => x.Name).SingleOrDefault();

            if (keyName == null)
                throw new InvalidOperationException("No primary key defined for the entity.");

            var keyValue = typeof(T).GetProperty(keyName)?.GetValue(entity);

            if (keyValue == null)
                throw new InvalidOperationException("The primary key value is null.");

            var existingEntity = await _context.Set<T>().FindAsync(keyValue);

            if (existingEntity == null)
                return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}