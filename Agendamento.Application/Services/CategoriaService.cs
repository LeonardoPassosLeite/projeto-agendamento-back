using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)

        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<CategoriaDTO> AddAsync(CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
                throw new ValidationException("Categoria não pode ser nulo.");

            if (!categoriaDto.IsActive)
                throw new ValidationException("Categoria não pode criada com status desativado.");

            try
            {
                var categoriaEntity = _mapper.Map<Categoria>(categoriaDto);
                await _categoriaRepository.AddAsync(categoriaEntity);
                return _mapper.Map<CategoriaDTO>(categoriaEntity);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao adicionar uma categoria", ex);
            }
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            try
            {
                var categoriaEntities = await _categoriaRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriaEntities);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar as categorias", ex);
            }
        }

        public async Task<CategoriaDTO> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id inválido.");
            }

            try
            {
                var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
                if (categoriaEntity == null)
                {
                    throw new NotFoundException($"Categoria com Id {id} não encontrado.");
                }

                return _mapper.Map<CategoriaDTO>(categoriaEntity);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar uma categoria por id", ex);
            }
        }

        public async Task<CategoriaDTO> UpdateAsync(UpdateCategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
                throw new ValidationException("Categoria não pode ser nulo");

            if (categoriaDto.Id <= 0)
                throw new ValidationException("Id inválido.");

            try
            {
                var categoriaEntity = await _categoriaRepository.GetByIdAsync(categoriaDto.Id);

                if (categoriaEntity == null)
                    throw new NotFoundException($"Categoria com Id {categoriaDto.Id} não encontrada.");

                _mapper.Map(categoriaDto, categoriaEntity);

                await _categoriaRepository.UpdateAsync(categoriaEntity);

                return _mapper.Map<CategoriaDTO>(categoriaEntity);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao editar uma categoria", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id inválido.");
            }

            try
            {
                var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
                if (categoriaEntity == null)
                {
                    throw new NotFoundException($"Categoria com Id {id} não encontrada.");
                }

                await _categoriaRepository.DeleteAsync(id);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao excluir a categoria", ex);
            }
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id inválido.");
            }

            try
            {
                var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
                if (categoriaEntity == null)
                {
                    throw new NotFoundException($"Categoria com Id {id} não encontrada.");
                }

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