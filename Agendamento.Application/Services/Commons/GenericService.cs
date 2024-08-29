using Agendamento.Application.DTOs.Commons;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Application.UseCases.Generics;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

public class GenericService<TEntity, TDto> : IGenericService<TDto>
    where TEntity : class
    where TDto : BaseDTO
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TDto> _validator;
    private readonly GetPagedUseCase<TEntity, TDto> _getPagedUseCase;
    private readonly AddUseCase<TEntity, TDto> _addUseCase;
    private readonly GetByIdUseCase<TEntity, TDto> _getByIdUseCase;
    private readonly UpdateUseCase<TEntity, TDto> _updateUseCase;
    private readonly DeleteUseCase<TEntity> _deleteUseCase;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _getPagedUseCase = new GetPagedUseCase<TEntity, TDto>(repository, mapper);
        _addUseCase = new AddUseCase<TEntity, TDto>(repository, mapper, validator);
        _getByIdUseCase = new GetByIdUseCase<TEntity, TDto>(repository, mapper);
        _updateUseCase = new UpdateUseCase<TEntity, TDto>(repository, mapper, validator);
        _deleteUseCase = new DeleteUseCase<TEntity>(repository);
    }

    public async Task<TDto> AddAsync(TDto dto)
    {
        return await _addUseCase.ExecuteAsync(dto);
    }

    public virtual async Task<PagedResultDTO<TDto>> GetPagedAsync(PaginationParams paginationParams)
    {
        return await _getPagedUseCase.ExecuteAsync(paginationParams);
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        return await _getByIdUseCase.ExecuteAsync(id);
    }

    public async Task<TDto> UpdateAsync(TDto dto)
    {
        return await _updateUseCase.ExecuteAsync(dto);
    }

    public async Task DeleteAsync(int id)
    {
        await _deleteUseCase.ExecuteAsync(id);
    }
}