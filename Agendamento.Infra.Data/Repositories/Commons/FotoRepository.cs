using Agendamento.Domain.Interfaces;
using Agendamento.Domain.Interfaces.Commons;
using Agendamento.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infra.Data.Repositories
{
    public abstract class FotoRepository<TEntity> : GenericRepository<TEntity>, IFotoRepository<TEntity>
        where TEntity : class, IFoto
    {
        protected FotoRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<bool> FotoPrincipalExistAsync(int id)
        {
            return await _context.Set<TEntity>().AnyAsync(f => EF.Property<int>(f, "Id") == id && f.IsPrincipal);
        }
    }
}
