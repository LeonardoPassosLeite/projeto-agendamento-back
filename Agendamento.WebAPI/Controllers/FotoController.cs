using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Exceptions;
using FluentValidation;
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

            try
            {
                var foto = await _fotoService.AddFotoAsync(fotoDto);
                return CreatedAtAction(nameof(GetFotoById), new { id = foto.Id }, foto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFotoById(int id)
        {
            try
            {
                var foto = await _fotoService.GetFotoByIdAsync(id);
                return Ok(foto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}