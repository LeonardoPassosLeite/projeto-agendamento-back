using Agendamento.Application.DTOs;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class UpdateProduto
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProdutoDTO> _validator;

    public UpdateProduto(IProdutoRepository produtoRepository, IMapper mapper, IValidator<ProdutoDTO> validator)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProdutoDTO> ExecuteAsync(ProdutoDTO produtoDto)
    {
        if (produtoDto == null)
            throw new ValidationException("Produto não pode ser nulo.");

        ValidationResult validationResult = await _validator.ValidateAsync(produtoDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var produto = await _produtoRepository.GetByIdAsync(produtoDto.Id);
        if (produto == null)
            throw new NotFoundException($"Produto com Id {produtoDto.Id} não encontrado.");

        if (produto.FotoPrincipal == null)
            throw new ConflictException("Não é possível editar um produto sem uma foto principal.");

        produto.Update(produtoDto.Nome, produtoDto.Preco, produtoDto.Descricao, produtoDto.CategoriaId);

        var updatedProduto = await _produtoRepository.UpdateAsync(produto);
        return _mapper.Map<ProdutoDTO>(updatedProduto);
    }
}