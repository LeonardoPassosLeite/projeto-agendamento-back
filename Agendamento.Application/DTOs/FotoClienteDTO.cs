using Microsoft.AspNetCore.Http;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities.Commons;
using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class FotoClienteDTO : FotoDTOBase
    {
        public int ClienteId { get; set; }
    }
}
