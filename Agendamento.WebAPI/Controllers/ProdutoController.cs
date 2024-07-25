using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("/api/[controller]")]
    public class ProdutoController : GenericController<IProdutoService, ProdutoDTO>
    {
        public ProdutoController(IProdutoService produtoService) : base(produtoService)
        { }

        [HttpGet("pageds")]
        public async Task<IActionResult> GetPageds([FromQuery] PaginationParams paginationParams)
        {
            var result = await _service.GetPagedProdutosAsync(paginationParams);
            return Ok(result);
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetByCategoriaId(int categoriaId)
        {
            if (categoriaId <= 0)
                return BadRequest(new { message = "Id de categoria deve ser maior que zero." });

            var produtos = await _service.GetProdutoByCategoriaIdAsync(categoriaId);
            return Ok(produtos);
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult> Disable(int id, [FromQuery] bool status)
        {
            var produtoDto = await _service.GetByIdAsync(id);
            if (produtoDto == null)
                return NotFound($"Produto com Id {id} não encontrado.");

            await _service.UpdateStatusProdutoAsync(id, status);
            return NoContent();
        }
    }
}