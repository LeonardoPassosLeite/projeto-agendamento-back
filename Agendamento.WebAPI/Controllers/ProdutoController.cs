using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAll()
        {
            try
            {
                var produtos = await _produtoService.GetProdutosAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            try
            {
                var produto = await _produtoService.GetByIdAsync(id);
                if (produto == null)
                {
                    return NotFound($"Produto com Id {id} não encontrada.");
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetByCategoriaId(int categoriaId)
        {
            try
            {
                var produtos = await _produtoService.GetByCategoriaIdAsync(categoriaId);
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Create([FromForm] ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var produto = await _produtoService.CreateProdutoAsync(produtoDto);
                return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Update(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (produtoDto == null || id != produtoDto.Id)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var produto = await _produtoService.UpdateProdutoAsync(produtoDto);
                if (produto == null)
                {
                    return NotFound($"Produto com Id {id} não encontrada.");
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPost("{id}/disable")]
        public async Task<ActionResult> Disable(int id)
        {
            try
            {
                var produtoDto = await _produtoService.GetByIdAsync(id);
                if (produtoDto == null)
                {
                    return NotFound($"Produto com Id {id} não encontrada.");
                }

                await _produtoService.DisableProdutoAsync(produtoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}