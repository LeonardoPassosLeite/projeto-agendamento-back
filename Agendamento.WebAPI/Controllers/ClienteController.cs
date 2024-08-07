using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : GenericController<IClienteService, ClienteFotoDTO>
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService) : base(clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ClienteFotoDTO>> ClienteProduto([FromBody] ClienteDTO dto)
        {
            var result = await _clienteService.AddCustomAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}