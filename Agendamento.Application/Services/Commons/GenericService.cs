using Agendamento.Application.DTOs.Commons;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class GenericService<TEntity, TDto> : IGenericService<TDto>
    where TEntity : class
    where TDto : BaseDTO
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TDto> _validator;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public virtual async Task<TDto> AddAsync(TDto dto)
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

    public virtual async Task<PagedResultDTO<TDto>> GetPagedAsync(PaginationParams paginationParams)
    {
        if (paginationParams == null)
            throw new ValidationException("Parâmetros de paginação não podem ser nulos.");

        try
        {
            var pagedResult = await _repository.GetPagedAsync(
                filter: null,
                page: paginationParams.Page,
                pageSize: paginationParams.PageSize,
                filterText: paginationParams.Filter
            );

            var itemsDto = _mapper.Map<IEnumerable<TDto>>(pagedResult.Items);

            return new PagedResultDTO<TDto>(itemsDto, pagedResult.TotalCount);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao obter a paginação.", ex);
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

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        if (dto == null)
            throw new ValidationException("DTO não pode ser nulo.");

        ValidationResult validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        try
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = await _repository.UpdateAsync(entity);
            if (updatedEntity == null)
                throw new NotFoundException($"Entidade com Id {dto.Id} não encontrada.");

            return _mapper.Map<TDto>(updatedEntity);
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
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Entidade com Id {id} não encontrada.");

            await _repository.DeleteAsync(id);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao deletar a entidade.", ex);
        }
    }
}