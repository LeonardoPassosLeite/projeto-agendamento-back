using System.ComponentModel.DataAnnotations;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;

public class UpdateStatusProduto
{
    private readonly IProdutoRepository _produtoRepository;

    public UpdateStatusProduto(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task ExecuteAsync(int id, bool isActive)
    {
        if (id <= 0)
            throw new ValidationException("Id inválido.");

        var produtoEntity = await _produtoRepository.GetByIdAsync(id);
        if (produtoEntity == null)
            throw new NotFoundException($"Produto com Id {id} não encontrado.");

        produtoEntity.ToggleAcitve();
        await _produtoRepository.UpdateAsync(produtoEntity);
    }
}
