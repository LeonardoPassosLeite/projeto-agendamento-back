using Agendamento.Application.DTOs.Commons;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public abstract class BaseUpdateUseCase<TEntity>
    where TEntity : class
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    protected BaseUpdateUseCase(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected async Task<TEntity> UpdateEntityAsync<TUpdateDto>(TUpdateDto dto, IValidator<TUpdateDto> validator)
        where TUpdateDto : BaseDTO
    {
        if (dto == null)
            throw new ValidationException("DTO não pode ser nulo.");

        ValidationResult validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var entity = _mapper.Map<TEntity>(dto);
        var updatedEntity = await _repository.UpdateAsync(entity);
        if (updatedEntity == null)
            throw new NotFoundException($"Entidade com Id {dto.Id} não encontrada.");

        return updatedEntity;
    }
}