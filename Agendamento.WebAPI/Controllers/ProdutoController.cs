using Microsoft.AspNetCore.Mvc;
using Agendamento.Application.Interfaces;
using Agendamento.Application.DTOs;

namespace Agendamento.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : GenericController<IProdutoService, ProdutoFotoDTO>
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService) : base(produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ProdutoFotoDTO>> AddProduto([FromBody] ProdutoDTO dto)
        {
            var result = await _produtoService.AddCustomAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatusProduto(int id, [FromBody] bool isActive)
        {
            await _produtoService.UpdateStatusProdutoAsync(id, isActive);
            return NoContent();
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosByCategoriaId(int categoriaId)
        {
            var produtos = await _produtoService.GetProdutosByCategoriaIdAsync(categoriaId);
            return Ok(produtos);
        }
    }
}