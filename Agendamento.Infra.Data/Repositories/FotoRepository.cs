using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Agendamento.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infrastructure.Repositories
{
    public class FotoRepository : GenericRepository<Foto>, IFotoRepository
    {
        public FotoRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<bool> FotoPrincipalExisteAsync(int produtoId)
        {
            return await _context.Fotos.AnyAsync(f => f.ProdutoId == produtoId && f.IsPrincial);
        }
    }
}