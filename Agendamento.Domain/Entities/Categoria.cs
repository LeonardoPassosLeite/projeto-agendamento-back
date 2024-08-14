namespace Agendamento.Domain.Entities
{
    public sealed class Categoria : SimpleEntity
    {
        public bool IsActive { get; set; } = true;
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria() { }
    }
}