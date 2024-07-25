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

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IPagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? filter = null, int page = 1, int pageSize = 10, string? filterText = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(filterText))
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Nome").ToLower(), $"%{filterText.ToLower()}%"));

            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDTO<T>(items, totalCount);
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