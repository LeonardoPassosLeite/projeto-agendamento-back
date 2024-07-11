using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IFotoService _fotoService;


        public ProdutoController(IProdutoService produtoService, IFotoService fotoService)
        {
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
            _fotoService = fotoService ?? throw new ArgumentNullException(nameof(fotoService));
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Add([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                var produto = await _produtoService.AddAsync(produtoDto);
                return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { messages = errorMessages });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoFotoDTO>>> GetAll()
        {
            try
            {
                var produtos = await _produtoService.GetAllAsync();
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
                return Ok(produto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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
                var produtos = await _produtoService.GetProdutoByCategoriaIdAsync(categoriaId);
                return Ok(produtos);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Update(int id, [FromForm] ProdutoUpdateDTO produtoDto)
        {
            if (id != produtoDto.Id)
                return BadRequest(new { messages = new List<string> { "O ID no caminho não corresponde ao ID no corpo da solicitação." } });

            try
            {
                var produto = await _produtoService.UpdateAsync(produtoDto);
                return Ok(produto);
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(error => error.ErrorMessage).ToList();

                return BadRequest(new { messages = errorMessages });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { messages = new List<string> { ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { messages = new List<string> { $"Ocorreu um erro interno: {ex.Message}" } });
            }
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult> Disable(int id, [FromQuery] bool status)
        {
            try
            {
                await _produtoService.UpdateStatusProdutoAsync(id, status);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno: {ex.Message}");
            }
        }
    }
}