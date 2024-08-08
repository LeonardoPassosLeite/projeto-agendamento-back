using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;

namespace Agendamento.Infra.Data.Repositories
{
    public class FotoProdutoRepository : FotoRepository<FotoProduto>, IFotoProdutoRepository
    {
        public FotoProdutoRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
