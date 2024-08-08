using Agendamento.Domain.Entities.Commons;

namespace Agendamento.Domain.Entities
{
    public sealed class FotoCliente : FotoBase
    {
        public int ClienteId { get; set; }
        public required Cliente? Cliente { get; set; }
    }
}