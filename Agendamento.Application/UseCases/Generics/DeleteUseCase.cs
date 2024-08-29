using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using FluentValidation;

namespace Agendamento.Application.UseCases.Generics
{
    public class DeleteUseCase<TEntity>
        where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public DeleteUseCase(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido.");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Entidade com Id {id} não encontrada.");

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
}