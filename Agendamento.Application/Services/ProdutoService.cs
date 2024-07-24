using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
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

    public async Task<IEnumerable<ProdutoFotoDTO>> GetAllProdutoFotosAsync()
    {
        var produtos = await _produtoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProdutoFotoDTO>>(produtos);
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