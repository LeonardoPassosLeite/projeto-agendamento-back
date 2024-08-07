using Agendamento.Application.DTOs.Commons;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces.Commons;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class CustomService<TEntity, TAddDto, TDto> : ICustomService<TAddDto, TDto>
    where TEntity : class
    where TAddDto : BaseDTO
    where TDto : BaseDTO
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TAddDto> _addValidator;

    public CustomService(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TAddDto> addValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _addValidator = addValidator;
    }

    public virtual async Task<TDto> AddCustomAsync(TAddDto dto)
    {
        if (dto == null)
            throw new ValidationException("DTO não pode ser nulo.");

        ValidationResult validationResult = await _addValidator.ValidateAsync(dto);

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

    public virtual async Task<TDto> AddAsync(TDto dto)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<TDto> GetByIdAsync(int id)
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

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        throw new NotImplementedException();
    }

    public virtual async Task DeleteAsync(int id)
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