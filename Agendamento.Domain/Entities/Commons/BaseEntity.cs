using System.Text.Json.Serialization;

namespace Agendamento.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public string Nome { get; protected set; } = string.Empty;
    }
}