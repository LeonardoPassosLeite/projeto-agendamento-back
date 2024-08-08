using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : ControllerBase
    {
        private readonly IFotoProdutoService _fotoProdutoService;
        private readonly IFotoClienteService _fotoClienteService;

        public FotoController(IFotoProdutoService fotoProdutoService,
                              IFotoClienteService fotoClienteService)
        {
            _fotoProdutoService = fotoProdutoService;
            _fotoClienteService = fotoClienteService;
        }

        [HttpPost("upload/produto")]
        public async Task<ActionResult<FotoProdutoDTO>> UploadFotoProduto([FromForm] FotoProdutoDTO fotoProdutoDto)
        {
            var result = await _fotoProdutoService.UploadFileAsync(fotoProdutoDto);
            return Ok(result);
        }
        
        [HttpPost("upload/cliente")]
        public async Task<ActionResult<FotoClienteDTO>> UploadFotoCliente([FromForm] FotoClienteDTO fotoClienteDto)
        {
            var result = await _fotoClienteService.UploadFileAsync(fotoClienteDto);
            return Ok(result);
        }
    }
}