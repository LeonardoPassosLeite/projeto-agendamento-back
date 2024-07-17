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
        private readonly IWebHostEnvironment _env;

        public FotosController(IFotoService fotoService, IWebHostEnvironment env)
        {
            _fotoService = fotoService;
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<FotoDTO>> AddFoto([FromBody] FotoDTO fotoDto)
        {
            if (fotoDto == null)
                return BadRequest("Foto não pode ser nula.");

            var foto = await _fotoService.AddFotoAsync(fotoDto);
            return CreatedAtAction(nameof(GetFotoById), new { id = foto.Id }, foto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<FotoDTO>> Update(int id, [FromBody] FotoDTO fotoDto)
        {
            if (id != fotoDto.Id)
                return BadRequest("ID inválido");

            var result = await _fotoService.UpdateFotoAsync(fotoDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFotoById(int id)
        {
            var foto = await _fotoService.GetFotoByIdAsync(id);
            return Ok(foto);
        }
    }
}