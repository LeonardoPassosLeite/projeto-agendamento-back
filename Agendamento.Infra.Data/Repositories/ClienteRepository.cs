using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;

namespace Agendamento.Infra.Data.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        // ApplicationDbContext _clienteContext;
        public ClienteRepository(ApplicationDbContext clienteContext) : base(clienteContext)
        {
            // _clienteContext = clienteContext;
        }

    }
}