using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : GenericController<ICategoriaService, CategoriaDTO>
    {
        public CategoriaController(ICategoriaService categoriaService) : base(categoriaService)
        { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaActiveDTO>>> GetAll()
        {
            return await GetAllCustom<CategoriaActiveDTO>();
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult> Disable(int id, [FromQuery] bool status)
        {
            var categoriaDto = await _service.GetByIdAsync(id);
            if (categoriaDto == null)
                return NotFound($"Categoria com Id {id} não encontrado.");

            await _service.UpdateStatusCategoriaAsync(id, status);
            return NoContent();
        }
    }
}