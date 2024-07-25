using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

public class ProdutoService : GenericService<Produto, ProdutoDTO>, IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly AddFotoToProduto _addFotoToProduto;
    private readonly GetPagedProdutos _getPagedProdutos;
    private readonly UpdateProduto _updateProduto;
    private readonly UpdateStatusProduto _updateStatusProduto;
    private readonly GetProdutoByCategoriaId _getProdutoByCategoriaId;

    public ProdutoService(IProdutoRepository produtoRepository, IGenericRepository<Produto> repository, IMapper mapper, IValidator<ProdutoDTO> validator,
                          AddFotoToProduto addFotoToProduto,
                          GetPagedProdutos getPagedProdutos,
                          UpdateProduto updateProduto,
                          UpdateStatusProduto updateStatusProduto,
                          GetProdutoByCategoriaId getProdutoByCategoriaId) : base(repository, mapper, validator)
    {
        _produtoRepository = produtoRepository;
        _addFotoToProduto = addFotoToProduto;
        _getPagedProdutos = getPagedProdutos;
        _updateProduto = updateProduto;
        _updateStatusProduto = updateStatusProduto;
        _getProdutoByCategoriaId = getProdutoByCategoriaId;
    }

    public override async Task<ProdutoDTO> UpdateAsync(ProdutoDTO dto)
    {
        await _updateProduto.ExecuteAsync(dto);

        var produtoAtualizado = await _produtoRepository.GetByIdAsync(dto.Id);
        return _mapper.Map<ProdutoDTO>(produtoAtualizado);
    }

    public Task AddFotoToProdutoAsync(int produtoId, FotoDTO fotoDto) => _addFotoToProduto.ExecuteAsync(produtoId, fotoDto);
    public Task<PagedResultDTO<ProdutoFotoDTO>> GetPagedProdutosAsync(PaginationParams paginationParams) => _getPagedProdutos.ExecuteAsync(paginationParams);
    public Task UpdateStatusProdutoAsync(int id, bool isActive) => _updateStatusProduto.ExecuteAsync(id, isActive);
    public Task<IEnumerable<ProdutoDTO>> GetProdutoByCategoriaIdAsync(int? id) => _getProdutoByCategoriaId.ExecuteAsync(id);
}