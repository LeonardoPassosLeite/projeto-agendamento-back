using Agendamento.Domain.Entities.Commons;

namespace Agendamento.Domain.Entities
{
    public sealed class FotoProduto : FotoBase
    {
        public int ProdutoId { get; set; }
        public required Produto Produto { get; set; }
    }
}
