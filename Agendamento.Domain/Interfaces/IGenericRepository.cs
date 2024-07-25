using System.Linq.Expressions;

namespace Agendamento.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IPagedResult<T>> GetPagedAsync(Expression<Func<T, bool>>? filter = null, int page = 1, int pageSize = 10, string? filterText = null);
        Task<T?> GetByIdAsync(int id);
        Task<T?> UpdateAsync(T? entity);
        Task DeleteAsync(int id);
    }

    public interface IPagedResult<T>
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
    }
}