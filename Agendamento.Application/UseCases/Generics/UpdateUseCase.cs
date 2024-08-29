using Agendamento.Application.DTOs.Commons;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

public class UpdateUseCase<TEntity, TDto> : BaseUpdateUseCase<TEntity>
    where TEntity : class
    where TDto : BaseDTO
{
    private readonly IValidator<TDto> _validator;

    public UpdateUseCase(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
        : base(repository, mapper)
    {
        _validator = validator;
    }

    public async Task<TDto> ExecuteAsync(TDto dto)
    {
        var updatedEntity = await UpdateEntityAsync(dto, _validator);
        return _mapper.Map<TDto>(updatedEntity);
    }
}