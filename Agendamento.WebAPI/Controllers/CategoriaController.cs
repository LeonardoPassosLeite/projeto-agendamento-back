using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : GenericController<ICategoriaService, CategoriaDTO>
    {
        public CategoriaController(ICategoriaService categoriaService) : base(categoriaService)
        { }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatus(int id, [FromQuery] bool status)
        {
            var categoriaDto = await _service.GetByIdAsync(id);
            if (categoriaDto == null)
                return NotFound($"Categoria com Id {id} não encontrado.");

            await _service.UpdateStatusCategoriaAsync(id, status);
            return NoContent();
        }
    }
}