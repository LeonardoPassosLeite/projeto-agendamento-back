using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

public class ProdutoService : GenericService<Produto, ProdutoDTO>, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly AddFotoToProduto _addFotoToProduto;
    private readonly UpdateStatusProduto _updateStatusProduto;
    private readonly UpdateProduto _updateProduto;
    private readonly GetProdutoByCategoriaId _getProdutoByCategoriaId;

    public ProdutoService(
        IProdutoRepository produtoRepository,
        IGenericRepository<Produto> repository,
        IMapper mapper,
        IValidator<ProdutoDTO> validator,
        AddFotoToProduto addFotoToProduto,
        UpdateStatusProduto updateStatusProduto,
        UpdateProduto updateProduto,
        GetProdutoByCategoriaId getProdutoByCategoriaId
    ) : base(repository, mapper, validator)
    {
        _produtoRepository = produtoRepository;
        _addFotoToProduto = addFotoToProduto;
        _updateStatusProduto = updateStatusProduto;
        _updateProduto = updateProduto;
        _getProdutoByCategoriaId = getProdutoByCategoriaId;
    }

    public async Task<PagedResultDTO<ProdutoFotoDTO>> GetPagedProdutosAsync(PaginationParams paginationParams)
    {
        if (paginationParams == null)
            throw new ValidationException("Parâmetros de paginação não podem ser nulos.");

        try
        {
            var pagedResult = await _produtoRepository.GetPagedAsync(
                page: paginationParams.Page,
                pageSize: paginationParams.PageSize,
                filterText: paginationParams.Filter
            );

            var produtoDtos = _mapper.Map<IEnumerable<ProdutoFotoDTO>>(pagedResult.Items);

            return new PagedResultDTO<ProdutoFotoDTO>(produtoDtos, pagedResult.TotalCount);
        }
        catch (DatabaseException ex)
        {
            throw new ApplicationException("Ocorreu um erro ao obter a paginação.", ex);
        }
    }

    public override async Task<ProdutoDTO> UpdateAsync(ProdutoDTO dto)
    {
        await _updateProduto.ExecuteAsync(dto);

        var produtoAtualizado = await _produtoRepository.GetByIdAsync(dto.Id);
        return _mapper.Map<ProdutoDTO>(produtoAtualizado);
    }

    public Task AddFotoToProdutoAsync(int produtoId, FotoDTO fotoDto) => _addFotoToProduto.ExecuteAsync(produtoId, fotoDto);
    public Task UpdateStatusProdutoAsync(int id, bool isActive) => _updateStatusProduto.ExecuteAsync(id, isActive);
    public Task<IEnumerable<ProdutoDTO>> GetProdutoByCategoriaIdAsync(int? id) => _getProdutoByCategoriaId.ExecuteAsync(id);
}