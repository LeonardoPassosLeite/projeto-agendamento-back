using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Application.Validators;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace Agendamento.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProdutoDTO> _validator;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, IValidator<ProdutoDTO> validator)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<ProdutoDTO> AddProdutoAsync(ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
                throw new ValidationException("Produto não pode ser nulo.");

            ValidationResult validationResult = await _validator.ValidateAsync(produtoDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            try
            {
                var produtoEntity = _mapper.Map<Produto>(produtoDto);
                await _produtoRepository.AddAsync(produtoEntity);
                return _mapper.Map<ProdutoDTO>(produtoEntity);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao adicionar um produto", ex);
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosAsync()
        {
            try
            {
                var produtoEntities = await _produtoRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ProdutoDTO>>(produtoEntities);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar os produtos", ex);
            }
        }

        public async Task<ProdutoDTO> GetProdutoByIdAsync(int? id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id inválido.");
            }

            try
            {
                var produtoEntity = await _produtoRepository.GetByIdAsync(id);
                if (produtoEntity == null)
                {
                    throw new NotFoundException($"Produto com Id {id} não encontrado.");
                }

                return _mapper.Map<ProdutoDTO>(produtoEntity);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar um produto por id", ex);
            }
        }

        public async Task<ProdutoDTO> UpdateProdutoAsync(ProdutoActiveDTO produtoDto)
        {
            if (produtoDto == null)
                throw new ValidationException("Produto não pode ser nulo");

            if (produtoDto.Id <= 0)
                throw new ValidationException("Id inválido.");

            var validator = new ProdutoActiveDTOValidator();
            ValidationResult validationResult = await validator.ValidateAsync(produtoDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            try
            {
                var produtoEntity = await _produtoRepository.GetByIdAsync(produtoDto.Id);

                if (produtoEntity == null)
                    throw new NotFoundException($"Produto com Id {produtoDto.Id} não encontrada.");

                _mapper.Map(produtoDto, produtoEntity);

                await _produtoRepository.UpdateAsync(produtoEntity);

                return _mapper.Map<ProdutoDTO>(produtoEntity);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao editar um produto", ex);
            }
        }

        public async Task UpdateStatusProdutoAsync(int id, bool isActive)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id inválido.");
            }

            try
            {
                var produtoEntity = await _produtoRepository.GetByIdAsync(id);
                if (produtoEntity == null)
                {
                    throw new NotFoundException($"Produto com Id {id} não encontrada.");
                }

                produtoEntity.IsActive = isActive;

                await _produtoRepository.UpdateAsync(produtoEntity);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao {(isActive ? "ativar" : "desativar")} o produto", ex);
            }
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutoByCategoriaIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Id da categoria não pode ser nulo.");
            }

            try
            {
                var produtos = await _produtoRepository.GetByCategoriaIdAsync(id.Value);
                if (!produtos.Any())
                {
                    throw new NotFoundException($"Nenhum produto encontrado para a categoria com Id {id.Value}.");
                }

                return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao obter os produtos da categoria com id {id}", ex);
            }
        }
    }
}