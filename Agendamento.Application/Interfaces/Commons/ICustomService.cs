using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.Interfaces
{
    public interface ICustomService<TAddDto, TDto> : IGenericService<TDto>
     where TAddDto : BaseDTO
     where TDto : BaseDTO
    {
        Task<TDto> AddCustomAsync(TAddDto dto);
        Task<TDto> UpdateCustomAsync(TAddDto dto);
    }
}