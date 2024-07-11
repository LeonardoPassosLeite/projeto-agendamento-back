using System.Linq.Expressions;

namespace Agendamento.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? orderBy = null);
        Task<T?> GetByIdAsync(int? id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}