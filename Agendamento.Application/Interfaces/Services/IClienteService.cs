using Agendamento.Application.DTOs;

namespace Agendamento.Application.Interfaces
{
    public interface IClienteService : ICustomService<ClienteDTO, ClienteFotoDTO>
    { }
}