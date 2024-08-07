using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class FuncionarioDTO : BaseInfosDTO
    {
        public string Cpf { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
    }

    public class FuncionarioDetailDTO : BaseInfosDTO
    {
        public string Empresa { get; set; } = string.Empty;
    }
}