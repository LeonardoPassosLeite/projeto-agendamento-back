using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace Agendamento.Application.Services
{
    public class ProdutoService : GenericService<Produto, ProdutoDTO>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, IValidator<ProdutoDTO> validatorProduto)
            : base(produtoRepository, mapper, validatorProduto)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public async Task PublishProductAsync(int produtoId)
        {
            var produtoEntity = await _produtoRepository.GetByIdAsync(produtoId);

            if (produtoEntity == null)
                throw new NotFoundException($"Produto com Id {produtoId} não encontrado.");

            produtoEntity.IsRascunho = false;
            await _produtoRepository.UpdateAsync(produtoEntity);
        }

        public async Task AddFotoToProdutoAsync(int produtoId, FotoDTO fotoDto)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);

            if (produto == null)
                throw new NotFoundException($"Produto com Id {produtoId} não encontrado.");

            var foto = _mapper.Map<Foto>(fotoDto);

            if (produto.FotoPrincipalId != null && produto.FotoPrincipalId == foto.Id)
                produto.FotoPrincipal = foto;
            else
                produto.SetFotoPrincipal(foto);

            await _produtoRepository.UpdateAsync(produto);
            await PublishProductAsync(produtoId);
        }

        public async Task UpdateStatusProdutoAsync(int id, bool isActive)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido.");

            try
            {
                var produtoEntity = await _produtoRepository.GetByIdAsync(id);
                if (produtoEntity == null)
                {
                    throw new NotFoundException($"Produto com Id {id} não encontrado.");
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
                throw new ValidationException("Id da categoria não pode ser nulo.");

            try
            {
                var produtos = await _produtoRepository.GetByCategoriaIdAsync(id.Value);
                if (!produtos.Any())
                    throw new NotFoundException($"Nenhum produto encontrado para a categoria com Id {id.Value}.");

                return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao obter os produtos da categoria com id {id}", ex);
            }
        }

        public override async Task<ProdutoDTO> UpdateAsync(ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
                throw new ValidationException("DTO não pode ser nulo.");


            ValidationResult validationResult = await _validator.ValidateAsync(produtoDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            try
            {
                var produto = await _produtoRepository.GetByIdAsync(produtoDto.Id);
                if (produto == null)
                    throw new NotFoundException($"Produto com Id {produtoDto.Id} não encontrado.");

                if (produto.FotoPrincipal == null)
                    throw new ConflictException("Não é possivel editar um produto sem uma foto principal");


                var fotoPrincipal = produto.FotoPrincipal;

                _mapper.Map(produtoDto, produto);
                produto.FotoPrincipal = fotoPrincipal;
                produto.IsRascunho = false;

                var updatedProduto = await _produtoRepository.UpdateAsync(produto);
                return _mapper.Map<ProdutoFotoDTO>(updatedProduto);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao atualizar o produto.", ex);
            }
        }
    }
}