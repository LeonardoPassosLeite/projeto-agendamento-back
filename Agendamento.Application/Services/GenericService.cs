using System.Linq.Expressions;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class GenericService<TEntity, TDto, TUpdateDto> where TEntity : class
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TDto> _validator;
    protected readonly IValidator<TUpdateDto> _updateValidator;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator, IValidator<TUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _updateValidator = updateValidator;
    }

    public async Task<TDto> AddAsync(TDto dto)
    {
        if (dto == null)
            throw new ValidationException("DTO não pode ser nulo.");

        ValidationResult validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao adicionar a entidade.", ex);
        }
    }

    public async Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TEntity, object>>? orderBy = null)
    {
        try
        {
            var entities = await _repository.GetAllAsync(orderBy);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao buscar as entidades", ex);
        }
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ValidationException("Id inválido.");

        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Entidade com Id {id} não encontrada.");

            return _mapper.Map<TDto>(entity);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao buscar a entidade por id.", ex);
        }
    }

    public async Task<TDto> UpdateAsync(TUpdateDto dto)
    {
        if (dto == null)
            throw new ValidationException("DTO não pode ser nulo.");

        ValidationResult validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<TDto>(entity);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao atualizar a entidade.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ValidationException("Id inválido.");

        try
        {
            await _repository.DeleteAsync(id);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao deletar a entidade.", ex);
        }
    }
}