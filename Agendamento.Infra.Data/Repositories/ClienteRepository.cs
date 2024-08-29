using System.Linq.Expressions;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infra.Data.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        ApplicationDbContext _clienteContext;
        public ClienteRepository(ApplicationDbContext clienteContext) : base(clienteContext)
        {
            _clienteContext = clienteContext;
        }

        public override async Task<Cliente?> GetByIdAsync(int id, params Expression<Func<Cliente, object>>[] includeProperties)
        {
            IQueryable<Cliente> query = _clienteContext.Clientes;

            query = query.Include(p => p.FotoPrincipal);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}