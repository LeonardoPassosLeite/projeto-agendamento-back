using System.Linq.Expressions;

namespace Agendamento.Application.Interfaces
{
    public interface IGenericService<TDto, TUpdateDto> where TDto : class
    {
        Task<TDto> AddAsync(TDto dto);
        // Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TDto, object>>? orderBy = null);
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> UpdateAsync(TUpdateDto dto);
        Task DeleteAsync(int id);
    }
}