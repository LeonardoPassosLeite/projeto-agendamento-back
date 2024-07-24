using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : ControllerBase
    {
        private readonly IFotoService _fotoService;
        private readonly IProdutoService _produtoService;

        public FotoController(IFotoService fotoService, IProdutoService produtoService)
        {
            _fotoService = fotoService;
            _produtoService = produtoService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FotoUploadDTO fotoUploadDto)
        {
            var fotoDto = await _fotoService.UploadFileAsync(fotoUploadDto);
            await _produtoService.AddFotoToProdutoAsync(fotoUploadDto.ProdutoId, fotoDto);

            return Ok(new { Id = fotoDto.Id, Url = fotoDto.Url });
        }
    }
}