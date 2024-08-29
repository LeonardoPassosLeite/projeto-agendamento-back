using Microsoft.AspNetCore.Mvc;
using Agendamento.Application.Interfaces;
using Agendamento.Application.DTOs;

namespace Agendamento.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : CustomController<IProdutoService, ProdutoDTO, ProdutoFotoDTO>
    {
        public ProdutoController(IProdutoService produtoService) : base(produtoService)
        { }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatusProduto(int id, [FromQuery] bool isActive)
        {
            await _customService.UpdateStatusProdutoAsync(id, isActive);
            return NoContent();
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosByCategoriaId(int categoriaId)
        {
            var produtos = await _customService.GetProdutosByCategoriaIdAsync(categoriaId);
            return Ok(produtos);
        }
    }
}
