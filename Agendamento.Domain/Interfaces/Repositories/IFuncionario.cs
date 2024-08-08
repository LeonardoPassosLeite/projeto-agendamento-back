using Agendamento.Domain.Enitiies;

namespace Agendamento.Domain.Interfaces
{
    public interface IFuncionario : IGenericRepository<Funcionario>
    {
        Task<IEnumerable<Funcionario>> GetByEmpresaIdAsync(int empresaId);
    }
}