using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosController : ControllerBase
    {
        private readonly IFotoService _fotoService;

        public FotosController(IFotoService fotoService)
        {
            _fotoService = fotoService;
        }

        [HttpPost]
        public async Task<ActionResult<FotoDTO>> AddFoto([FromBody] FotoDTO fotoDto)
        {
            if (fotoDto == null)
                return BadRequest("Foto não pode ser nula.");

            var foto = await _fotoService.AddFotoAsync(fotoDto);
            return CreatedAtAction(nameof(GetFotoById), new { id = foto.Id }, foto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFotoById(int id)
        {
            var foto = await _fotoService.GetFotoByIdAsync(id);
            return Ok(foto);
        }
    }
}
