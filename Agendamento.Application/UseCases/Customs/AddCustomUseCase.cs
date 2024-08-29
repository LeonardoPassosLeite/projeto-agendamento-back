using Agendamento.Application.DTOs.Commons;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace Agendamento.Application.UseCases.Generics
{
    public class AddCustomUseCase<TEntity, TAddDto, TDto>
        where TEntity : class
        where TAddDto : BaseDTO
        where TDto : BaseDTO
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<TAddDto> _validator;

        public AddCustomUseCase(IGenericRepository<TEntity> repository, IMapper mapper, IValidator<TAddDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<TDto> ExecuteAsync(TAddDto dto)
        {
            if (dto == null)
                throw new ValidationException("DTO não pode ser nulo.");

            ValidationResult validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                await _repository.AddAsync(entity);
                return _mapper.Map<TDto>(entity);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao adicionar a entidade.", ex);
            }
        }
    }
}