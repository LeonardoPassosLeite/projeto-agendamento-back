namespace Agendamento.Domain.Enitiies
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public string Nome { get; protected set; }
    }
}