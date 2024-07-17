using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.Services
{
    public class CategoriaService : GenericService<Categoria, CategoriaDTO>, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, IValidator<CategoriaDTO> validatorProduto)
            : base(categoriaRepository, mapper, validatorProduto)
        {
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
        }

        public async Task UpdateStatusCategoriaAsync(int id, bool isActive)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido.");

            try
            {
                var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
                if (categoriaEntity == null)
                    throw new NotFoundException($"Categoria com Id {id} não encontrada.");

                categoriaEntity.IsActive = isActive;
                await _categoriaRepository.UpdateAsync(categoriaEntity);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao {(isActive ? "ativar" : "desativar")} a categoria", ex);
            }
        }
    }
}