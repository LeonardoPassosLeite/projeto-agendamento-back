using Agendamento.Application.Helpers;

namespace Agendamento.Application.Interfaces
{
    public interface IGenericService<TDto> where TDto : class
    {
        Task<TDto> AddAsync(TDto dto);
        Task<PagedResultDTO<TDto>> GetPagedAsync(PaginationParams paginationParams);
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }
}
