using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces.Commons;

namespace Agendamento.Application.Interfaces
{
    public interface IClienteService : ICustomService<ClienteDTO, ClienteFotoDTO>
    { }
}