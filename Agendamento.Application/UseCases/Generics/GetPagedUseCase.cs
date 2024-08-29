using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Agendamento.Application.Helpers;
using Agendamento.Domain.Interfaces;
using AutoMapper;

public class GetPagedUseCase<TEntity, TDto>
    where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GetPagedUseCase(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResultDTO<TDto>> ExecuteAsync(
        PaginationParams paginationParams,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        if (paginationParams == null)
            throw new ValidationException("Parâmetros de paginação não podem ser nulos.");

        var pagedResult = await _repository.GetPagedAsync(
            filter: null,
            page: paginationParams.Page,
            pageSize: paginationParams.PageSize,
            filterText: paginationParams.Filter,
            includeProperties: includeProperties
        );

        var itemsDto = _mapper.Map<IEnumerable<TDto>>(pagedResult.Items);

        return new PagedResultDTO<TDto>(itemsDto, pagedResult.TotalCount);
    }
}