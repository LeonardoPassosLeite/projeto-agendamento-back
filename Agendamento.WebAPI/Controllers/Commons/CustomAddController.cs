using Microsoft.AspNetCore.Mvc;
using Agendamento.Application.DTOs.Commons;
using Agendamento.Application.Interfaces;

namespace Agendamento.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CustomAddController<TService, TDto> : GenericController<TService, TDto>
        where TService : IGenericService<TDto>
        where TDto : BaseDTO
    {
        protected readonly TService _customService;

        protected CustomAddController(TService service) : base(service)
        {
            _customService = service ?? throw new ArgumentNullException(nameof(service)); ;
        }

        [HttpPost("add")]
        public async Task<ActionResult<TDto>> Add([FromBody] TDto dto)
        {
            var result = await _customService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}