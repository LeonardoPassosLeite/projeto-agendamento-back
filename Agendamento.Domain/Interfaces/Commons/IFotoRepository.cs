namespace Agendamento.Domain.Interfaces.Commons
{
    public interface IFotoRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> FotoPrincipalExistAsync(int id);
    }
}