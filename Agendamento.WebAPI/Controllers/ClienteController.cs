using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : CustomController<IClienteService, ClienteDTO, ClienteFotoDTO>
    {
        public ClienteController(IClienteService clienteService) : base(clienteService)
        { }
    }
}
