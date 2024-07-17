namespace Agendamento.Application.Interfaces
{
    public interface IGenericService<TDto> where TDto : class
    {
        Task<TDto> AddAsync(TDto dto);
        Task<IEnumerable<TResponseDto>> GetAllAsync<TResponseDto>() where TResponseDto : class;
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }
}
