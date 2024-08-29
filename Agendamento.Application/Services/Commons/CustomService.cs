using Agendamento.Application.DTOs.Commons;
using Agendamento.Application.Interfaces;
using Agendamento.Application.UseCases.Customs;
using Agendamento.Application.UseCases.Generics;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

public class CustomService<TEntity, TAddDto, TDto> : GenericService<TEntity, TDto>, ICustomService<TAddDto, TDto>
    where TEntity : class
    where TAddDto : BaseDTO
    where TDto : BaseDTO
{
    private readonly AddCustomUseCase<TEntity, TAddDto, TDto> _addCustomUseCase;
    private readonly UpdateCustomUseCase<TEntity, TAddDto, TDto> _updateCustomUseCase;


    public CustomService(
        IGenericRepository<TEntity> repository,
        IMapper mapper,
        IValidator<TAddDto> addValidator,
        IValidator<TDto> validator)
        : base(repository, mapper, validator)
    {
        _addCustomUseCase = new AddCustomUseCase<TEntity, TAddDto, TDto>(repository, mapper, addValidator);
        _updateCustomUseCase = new UpdateCustomUseCase<TEntity, TAddDto, TDto>(repository, mapper, addValidator);
    }

    public virtual async Task<TDto> AddCustomAsync(TAddDto dto)
    {
        return await _addCustomUseCase.ExecuteAsync(dto);
    }

    public virtual async Task<TDto> UpdateCustomAsync(TAddDto dto)
    {
        return await _updateCustomUseCase.ExecuteAsync(dto);
    }
}
