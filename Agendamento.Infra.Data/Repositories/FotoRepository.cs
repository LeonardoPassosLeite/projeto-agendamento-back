using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Agendamento.Infra.Data.Repositories;

namespace Agendamento.Infrastructure.Repositories
{
    public class FotoRepository : GenericRepository<Foto>, IFotoRepository
    {
        public FotoRepository(ApplicationDbContext context) : base(context)
        { }
    }
}