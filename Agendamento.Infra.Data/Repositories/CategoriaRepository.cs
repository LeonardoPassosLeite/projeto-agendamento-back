using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;

namespace Agendamento.Infra.Data.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        ApplicationDbContext _categoriaContext;
        public CategoriaRepository(ApplicationDbContext categoriaContext) : base(categoriaContext)
        {
            _categoriaContext = categoriaContext;
        }

        public async Task DisableAsync(Categoria categoria)
        {
            _categoriaContext.Categorias.Update(categoria);
            await _categoriaContext.SaveChangesAsync();
        }
    }
}