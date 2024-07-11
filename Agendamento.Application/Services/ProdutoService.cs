using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.Services
{
    public class ProdutoService : GenericService<Produto, ProdutoDTO, ProdutoUpdateDTO>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, IValidator<ProdutoDTO> validatorProduto, IValidator<ProdutoUpdateDTO> validatorProdutoActive)
            : base(produtoRepository, mapper, validatorProduto, validatorProdutoActive)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public async Task<IEnumerable<ProdutoFotoDTO>> GetAllAsync()
        {
            try
            {
                var produtoEntities = await _produtoRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ProdutoFotoDTO>>(produtoEntities);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar os produtos", ex);
            }
        }

        public async Task FinalizarProdutoAsync(int produtoId)
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

            await FinalizarProdutoAsync(produtoId);
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
    }
}