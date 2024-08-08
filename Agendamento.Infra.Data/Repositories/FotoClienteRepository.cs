using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;

namespace Agendamento.Infra.Data.Repositories
{
    public class FotoClienteRepository : FotoRepository<FotoCliente>, IFotoClienteRepository
    {
        public FotoClienteRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
