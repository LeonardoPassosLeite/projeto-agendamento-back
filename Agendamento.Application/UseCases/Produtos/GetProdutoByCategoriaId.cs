using System.ComponentModel.DataAnnotations;
using Agendamento.Application.DTOs;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;

public class GetProdutoByCategoriaId
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public GetProdutoByCategoriaId(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoFotoDTO>> ExecuteAsync(int? id)
    {
        if (id == null)
            throw new ValidationException("Id da categoria não pode ser nulo.");

        var produtos = await _produtoRepository.GetByCategoriaIdAsync(id.Value);
        if (!produtos.Any())
            throw new NotFoundException($"Nenhum produto encontrado para a categoria com Id {id.Value}.");

        return _mapper.Map<IEnumerable<ProdutoFotoDTO>>(produtos);
    }
}
