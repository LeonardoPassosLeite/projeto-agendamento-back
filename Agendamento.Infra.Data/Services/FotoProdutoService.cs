using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Application.Services.Commons;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;

public class FotoProdutoService : FotoServiceBase<FotoProduto, FotoProdutoDTO>, IFotoProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public FotoProdutoService(IConfiguration configuration,
                              IFotoProdutoRepository fotoRepository,
                              IProdutoRepository produtoRepository,
                              IMapper mapper,
                              IValidator<FotoProdutoDTO> validator)
        : base(configuration, fotoRepository, mapper, validator)
    {
        _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public async Task<FotoProdutoDTO> UploadFileAsync(FotoProdutoDTO fotoProdutoUploadDto)
    {
        return await base.UploadFileAsync(fotoProdutoUploadDto, async (foto) =>
        {
            var produto = await _produtoRepository.GetByIdAsync(fotoProdutoUploadDto.ProdutoId);
            if (produto == null)
                throw new NotFoundException($"Produto com Id {fotoProdutoUploadDto.ProdutoId} não encontrado.");


            if (!produto.IsRascunho && produto.FotoPrincipalId.HasValue)
                throw new ConflictException($"O produto com Id {fotoProdutoUploadDto.ProdutoId} já possui uma foto principal.");

            produto.SetFotoPrincipal(foto);
            await _produtoRepository.UpdateAsync(produto);
        });
    }
}