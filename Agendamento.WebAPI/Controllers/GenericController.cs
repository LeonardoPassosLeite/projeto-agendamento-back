using Microsoft.AspNetCore.Mvc;
using Agendamento.Application.Interfaces;
using Agendamento.Application.DTOs.Commons;

namespace Agendamento.WebAPI.Controllers
{
    public abstract class GenericController<TService, TDto> : ControllerBase
        where TService : IGenericService<TDto>
        where TDto : BaseDTO
    {
        protected readonly TService _service;

        protected GenericController(TService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public async Task<ActionResult<TDto>> Add([FromBody] TDto dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<TResponseDto>>> GetAllCustom<TResponseDto>() where TResponseDto : class
        {
            var items = await _service.GetAllAsync<TResponseDto>();
            return Ok(items);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TDto>> Update(int id, [FromBody] TDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID inválido");

            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}