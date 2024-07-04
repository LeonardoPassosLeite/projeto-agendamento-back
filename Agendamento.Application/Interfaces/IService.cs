namespace Agendamento.Application.Interfaces
{
    public interface IService<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> AddAsync(TDto dto);
        // Task<TDto> UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }
}
