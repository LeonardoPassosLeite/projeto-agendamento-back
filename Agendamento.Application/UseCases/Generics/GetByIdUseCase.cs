using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using System.Linq.Expressions;

public class GetByIdUseCase<TEntity, TDto>
    where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GetByIdUseCase(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDto> ExecuteAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        if (id <= 0)
            throw new ValidationException("Id inválido.");

        try
        {
            var entity = await _repository.GetByIdAsync(id, includeProperties);
            if (entity == null)
                throw new NotFoundException($"Entidade com Id {id} não encontrada.");

            return _mapper.Map<TDto>(entity);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao buscar a entidade por id.", ex);
        }
    }
}