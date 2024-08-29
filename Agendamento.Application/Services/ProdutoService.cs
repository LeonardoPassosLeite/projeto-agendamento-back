using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Application.UseCases.Customs;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.Services
{
    public class ProdutoService : CustomService<Produto, ProdutoDTO, ProdutoFotoDTO>, IProdutoService
    {
        private readonly UpdateStatusProduto _updateStatusProduto;
        private readonly GetProdutoByCategoriaId _getProdutoByCategoriaId;
        private readonly GetPagedUseCase<Produto, ProdutoFotoDTO> _getPagedUseCase;
        private readonly UpdateCustomUseCase<Produto, ProdutoDTO, ProdutoFotoDTO> _updateProdutoUseCase;

        public ProdutoService(
            IProdutoRepository produtoRepository,
            IMapper mapper,
            IValidator<ProdutoDTO> addValidator,
            IValidator<ProdutoFotoDTO> validator)
            : base(produtoRepository, mapper, addValidator, validator)
        {
            _updateStatusProduto = new UpdateStatusProduto(produtoRepository);
            _getProdutoByCategoriaId = new GetProdutoByCategoriaId(produtoRepository, mapper);
            _getPagedUseCase = new GetPagedUseCase<Produto, ProdutoFotoDTO>(produtoRepository, mapper);
            _updateProdutoUseCase = new UpdateCustomUseCase<Produto, ProdutoDTO, ProdutoFotoDTO>(produtoRepository, mapper, addValidator);

        }

        public override async Task<PagedResultDTO<ProdutoFotoDTO>> GetPagedAsync(PaginationParams paginationParams)
        {
            return await _getPagedUseCase.ExecuteAsync(paginationParams, p => p.FotoPrincipal!, p => p.Categoria);
        }

        public override async Task<ProdutoFotoDTO> UpdateCustomAsync(ProdutoDTO dto)
        {
            return await _updateProdutoUseCase.ExecuteAsync(dto);
        }

        public async Task UpdateStatusProdutoAsync(int id, bool isActive)
        {
            await _updateStatusProduto.ExecuteAsync(id, isActive);
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosByCategoriaIdAsync(int? id)
        {
            return await _getProdutoByCategoriaId.ExecuteAsync(id);
        }
    }
}