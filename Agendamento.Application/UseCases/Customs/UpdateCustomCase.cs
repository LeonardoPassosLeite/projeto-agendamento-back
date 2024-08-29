using Agendamento.Application.DTOs.Commons;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.UseCases.Customs
{
    public class UpdateCustomUseCase<TEntity, TUpdateDto, TReturnDto> : BaseUpdateUseCase<TEntity>
      where TEntity : class
      where TUpdateDto : BaseDTO
      where TReturnDto : BaseDTO
    {
        private readonly IValidator<TUpdateDto> _validator;

        public UpdateCustomUseCase(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TUpdateDto> validator)
            : base(repository, mapper)
        {
            _validator = validator;
        }

        public async Task<TReturnDto> ExecuteAsync(TUpdateDto dto)
        {
            var updatedEntity = await UpdateEntityAsync(dto, _validator);
            return _mapper.Map<TReturnDto>(updatedEntity);
        }
    }
}