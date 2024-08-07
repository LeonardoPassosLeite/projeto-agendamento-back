namespace Agendamento.Domain.Interfaces.Commons
{
    public interface ICustomRepository<T> : IGenericRepository<T> where T : class
    {
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? filterText);
    }
}