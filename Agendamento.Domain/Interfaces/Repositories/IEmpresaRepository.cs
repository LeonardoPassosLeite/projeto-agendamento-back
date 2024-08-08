using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Interfaces
{
    public interface IEmpresaRepository : IGenericRepository<Empresa>
    {
        Task DisableAsync(Empresa empresa);
        Task<(IEnumerable<Empresa> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? filterText);
    }
}