using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class ClienteDTO : BaseInfosDTO
    {
        public string Cpf { get; set; } = string.Empty;
    }

    public class ClienteFotoDTO : BaseInfosDTO
    {
        public bool isVisit { get; set; }
        public FotoClienteDTO? FotoPrincipal { get; set; }
    }
}
