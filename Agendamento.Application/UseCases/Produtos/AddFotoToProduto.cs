using Agendamento.Application.DTOs;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;

public class AddFotoToProduto
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public AddFotoToProduto(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(int produtoId, FotoDTO fotoDto)
    {
        var produto = await _produtoRepository.GetByIdAsync(produtoId);
        if (produto == null)
            throw new NotFoundException($"Produto com Id {produtoId} não encontrado.");

        var foto = _mapper.Map<Foto>(fotoDto);

        produto.SetFotoPrincipal(foto);

        await _produtoRepository.UpdateAsync(produto);
    }
}