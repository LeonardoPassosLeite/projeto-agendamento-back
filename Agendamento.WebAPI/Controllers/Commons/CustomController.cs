using Microsoft.AspNetCore.Mvc;
using Agendamento.Application.Interfaces;
using Agendamento.Application.DTOs.Commons;

namespace Agendamento.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CustomController<TService, TAddDto, TDto> : GenericController<TService, TDto>
        where TService : ICustomService<TAddDto, TDto>
        where TAddDto : BaseDTO
        where TDto : BaseDTO
    {
        protected readonly TService _customService;

        protected CustomController(TService service) : base(service)
        {
            _customService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost("add")]
        public async Task<ActionResult<TDto>> AddCustom([FromBody] TAddDto dto)
        {
            var result = await _customService.AddCustomAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TDto>> UpdateCustom(int id, [FromBody] TAddDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID inválido");

            var result = await _customService.UpdateCustomAsync(dto);
            return Ok(result);
        }
    }
}