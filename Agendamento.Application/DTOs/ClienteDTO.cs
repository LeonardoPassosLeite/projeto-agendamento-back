using Agendamento.Application.DTOs.Commons;
using Agendamento.Domain.Entities;

namespace Agendamento.Application.DTOs
{
    public class ClienteDTO : BaseInfosDTO
    {
        public string Cpf { get; set; } = string.Empty;
    }

    public class ClienteFotoDTO : BaseInfosDTO
    {
        public bool isVisit { get; set; }
        public Foto? FotoPrincipal { get; set; }
    }
}
