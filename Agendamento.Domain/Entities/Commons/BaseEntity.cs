namespace Agendamento.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; protected set; } = DateTime.UtcNow;
        public DateTime DataAtualizacao { get; protected set; } = DateTime.UtcNow;

    }
    public class SimpleEntity : BaseEntity
    {
        public string Nome { get; protected set; } = string.Empty;
    }
}