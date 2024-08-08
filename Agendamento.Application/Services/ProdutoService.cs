using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace Agendamento.Application.Services
{
    public class ProdutoService : CustomService<Produto, ProdutoDTO, ProdutoFotoDTO>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly UpdateProduto _updateProduto;
        private readonly UpdateStatusProduto _updateStatusProduto;
        private readonly GetProdutoByCategoriaId _getProdutoByCategoriaId;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, IValidator<ProdutoDTO> addValidator)
            : base(produtoRepository, mapper, addValidator)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
            _updateProduto = new UpdateProduto(produtoRepository, mapper, addValidator);
            _updateStatusProduto = new UpdateStatusProduto(produtoRepository);
            _getProdutoByCategoriaId = new GetProdutoByCategoriaId(produtoRepository, mapper);
        }

        public override async Task<PagedResultDTO<ProdutoFotoDTO>> GetPagedAsync(PaginationParams paginationParams)
        {
            if (paginationParams == null)
                throw new ValidationException("Parâmetros de paginação não podem ser nulos.");

            var pagedResult = await _produtoRepository.GetPagedAsync(
                filter: null,
                page: paginationParams.Page,
                pageSize: paginationParams.PageSize,
                filterText: paginationParams.Filter,
                p => p.FotoPrincipal!
            );

            var itemsDto = _mapper.Map<IEnumerable<ProdutoFotoDTO>>(pagedResult.Items);

            return new PagedResultDTO<ProdutoFotoDTO>(itemsDto, pagedResult.TotalCount);
        }

        public override async Task<ProdutoFotoDTO> AddCustomAsync(ProdutoDTO dto)
        {
            if (dto == null)
                throw new ValidationException("DTO não pode ser nulo.");

            ValidationResult validationResult = await _addValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            try
            {
                var entity = _mapper.Map<Produto>(dto);
                await _produtoRepository.AddAsync(entity);
                return _mapper.Map<ProdutoFotoDTO>(entity);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao adicionar a entidade.", ex);
            }
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